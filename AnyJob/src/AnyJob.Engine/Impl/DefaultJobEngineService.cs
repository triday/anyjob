using AnyJob.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultJobEngineService : IJobEngineService
    {


        public DefaultJobEngineService(ILogger<DefaultJobEngineService> logger, IActionExecuterService actionExecuterService, IIdGenService idGenService)
        {
            this.logger = logger;
            this.idGenService = idGenService;
            this.actionExecuterService = actionExecuterService;
        }

        private IIdGenService idGenService;

        private ILogger logger;

        private IActionExecuterService actionExecuterService;

        #region 字段
        private const int MAX_JOB_COUNT = 100;
        private ConcurrentDictionary<string, Job> currentJobs = new ConcurrentDictionary<string, Job>();
        #endregion





        #region 取消任务
        public bool Cancel(string executionId)
        {
            if (currentJobs.TryGetValue(executionId, out var job))
            {
                this.logger.LogInformation($"Begin cancel execution [{executionId}]");
                job.Spy.Cancel();
                return true;
            }
            else
            {
                this.logger.LogWarning($"Can not cancel execution [{executionId}]");
                return false;
            }
        }
        #endregion

        #region 开始任务
        public Job Start(JobStartInfo jobStartInfo)
        {
            if (jobStartInfo == null)
            {
                throw new ArgumentNullException(nameof(jobStartInfo));
            }

            lock (this)
            {
                if (this.currentJobs.Count >= MAX_JOB_COUNT)
                {
                    logger.LogError($"Maximizing jobs limit, total count {this.currentJobs.Count}.");
                    throw Errors.JobCountLimitError(MAX_JOB_COUNT);
                }
                var spy = this.OnCreateSpy(jobStartInfo);
                var executePath = this.OnCreateExecutePath(jobStartInfo);
                var context = this.OnCreateExecuteContext(jobStartInfo, spy.Token, executePath);
                var task = actionExecuterService.Execute(context);
                var jobinfo = new Job() { ExecutionId = executePath.ExecuteId, StartInfo = jobStartInfo, Spy = spy, Task = task };
                currentJobs[executePath.ExecuteId] = jobinfo;
                this.logger.LogDebug($"Add jobInfo in engine [{ executePath.ExecuteId}]");
                task.ContinueWith((e) =>
                {
                    if (currentJobs.TryRemove(executePath.ExecuteId, out var job))
                    {
                        this.logger.LogDebug($"Remove jobInfo in engine [{ executePath.ExecuteId}].");
                    }
                    else
                    {
                        this.logger.LogWarning($"Can not remove job info in engine [{ executePath.ExecuteId}].");
                    }
                });
                return jobinfo;
            }
        }
        protected virtual IJobSpy OnCreateSpy(JobStartInfo jobStartInfo)
        {
            return new JobSpy();
        }

        protected virtual IExecutePath OnCreateExecutePath(JobStartInfo jobStartInfo)
        {
            if (string.IsNullOrEmpty(jobStartInfo.ExecutionId))
            {

                var newid = this.idGenService.NewId();
                return ExecutePath.RootPath(newid);
            }
            else
            {
                return ExecutePath.RootPath(jobStartInfo.ExecutionId);
            }
        }

        protected virtual IExecuteContext OnCreateExecuteContext(JobStartInfo jobStartInfo, CancellationToken token, IExecutePath path)
        {

            var parameters = new ExecuteParameter()
            {
                Context = new ReadOnlyDictionary<string, object>(jobStartInfo.Context ?? new Dictionary<string, object>()),
                Inputs = new ReadOnlyDictionary<string, object>(jobStartInfo.Inputs ?? new Dictionary<string, object>()),
                Vars = new ConcurrentDictionary<string, object>(),
                GlobalVars = new ConcurrentDictionary<string, object>(),
            };
            return new ExecuteContext
            {
                ActionFullName = jobStartInfo.ActionFullName,
                ExecuteParameter = parameters,
                ExecutePath = path,
                Token = token,
                ActionRetryCount = jobStartInfo.RetryCount
            };
        }
        #endregion



    }


}
