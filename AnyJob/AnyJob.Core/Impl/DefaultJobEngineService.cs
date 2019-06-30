using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AnyJob.Impl
{

    public class DefaultJobEngineService : IJobEngineService
    {


        public DefaultJobEngineService(ILogService logService, IActionExecuterService actionExecuterService, IIdGenService idGenService)
        {
            this.logService = logService;
            this.idGenService = idGenService;
            this.actionExecuterService = actionExecuterService;
        }

        private IIdGenService idGenService;

        private ILogService logService;

        private IActionExecuterService actionExecuterService;

        #region 字段
        private const int MAX_JOB_COUNT = 100;
        private ConcurrentDictionary<string, Job> currentJobs;
        #endregion





        #region 取消任务
        public bool Cancel(string executionId)
        {
            if (currentJobs.TryGetValue(executionId, out var job))
            {
                this.logService.Info($"Begin cancel execution [{executionId}]");
                job.Spy.Cancel();
                return true;
            }
            else
            {
                this.logService.Warn($"Can not cancel execution [{executionId}]");
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
                    logService.Error($"Maximizing jobs limit, total count {this.currentJobs.Count}.");
                    throw ActionException.FromErrorCode(nameof(ErrorCodes.JobCountLimit), MAX_JOB_COUNT);
                }
                var spy = this.OnCreateSpy(jobStartInfo);
                var executePath = this.OnCreateExecutePath(jobStartInfo);
                var context = this.OnCreateExecuteContext(jobStartInfo, spy.Token, executePath);
                var task = actionExecuterService.Execute(context);
                var jobinfo = new Job() { ExecutionId = executePath.ExecuteId, StartInfo = jobStartInfo, Spy = spy, Task = task };
                currentJobs[executePath.ExecuteId] = jobinfo;
                logService.Debug($"Add jobInfo in engine [{ executePath.ExecuteId}]");
                task.ContinueWith((e) =>
                {
                    if (currentJobs.TryRemove(executePath.ExecuteId, out var job))
                    {
                        logService.Debug($"Remove jobInfo in engine [{ executePath.ExecuteId}].");
                    }
                    else
                    {
                        logService.Warn($"Can not remove job info in engine [{ executePath.ExecuteId}].");
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

            var parameters = new ActionParameters(jobStartInfo.Inputs, jobStartInfo.Context);
            return new ExecuteContext
            {
                ActionFullName = jobStartInfo.ActionFullName,
                ActionParameters = parameters,
                ExecutePath = path,
                Token = token,
                ActionRetryCount = jobStartInfo.RetryCount
            };
        }
        #endregion



    }

    public class TypeFilterEventArgs : EventArgs
    {
        public TypeFilterEventArgs(Type serviceType, Type interfaceType)
        {
            this.InterfaceType = interfaceType;
            this.ServiceType = serviceType;
        }
        public Type ServiceType { get; private set; }
        public Type InterfaceType { get; private set; }
        public bool Filtered { get; set; }
    }
}
