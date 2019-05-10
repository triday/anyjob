using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Impl
{

    public class JobEngine : IJobEngine, IDisposable
    {
       
        public JobEngine(bool autoRegisterCurrentDomainServices = true)
        {
            this.currentJobs = new ConcurrentDictionary<string, Job>(StringComparer.CurrentCultureIgnoreCase);
            this.services = new ServiceCollection();
            if (autoRegisterCurrentDomainServices)
            {
                this.RegisterCurrentDomainServices();
            }
            else
            {
                this.provider = services.BuildServiceProvider();
            }

        }

        public event EventHandler<TypeFilterEventArgs> FilterAssemblyType;

        #region 字段
        private const int MAX_JOB_COUNT = 100;
        private ConcurrentDictionary<string, Job> currentJobs;
        private ServiceCollection services;
        private IServiceProvider provider;
        #endregion

        #region 注入服务
        public void RegisterServices(Action<IServiceCollection> config)
        {
            if (config != null)
            {
                config(this.services);
            }
            this.provider = services.BuildServiceProvider();
        }
        public void RegisterCurrentDomainServices()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                RegisterServices((services) =>
                {
                    services.ConfigAssemblyServices(assembly, (serviceType, interfaceType) =>
                    {
                        var args = new TypeFilterEventArgs(interfaceType, serviceType);
                        this.OnFilterAssemblyType(args);
                        return args.Filtered;
                    });

                });
            }
        }
        protected virtual void OnFilterAssemblyType(TypeFilterEventArgs e)
        {
            if (this.FilterAssemblyType != null)
            {
                this.FilterAssemblyType(this, e);
            }
        }
        #endregion

        #region 取消任务
        public bool Cancel(string executionId)
        {
            var logger = this.provider.GetRequiredService<ILogService>();
            if (currentJobs.TryGetValue(executionId, out var job))
            {
                logger.Info($"Begin cancel execution [{executionId}]");
                job.Spy.Cancel();
                return true;
            }
            else
            {
                logger.Warn($"Can not cancel execution [{executionId}]");
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
            var executer = this.provider.GetRequiredService<IActionExecuterService>();
            var logger = this.provider.GetRequiredService<ILogService>();
            lock (this)
            {
                if (this.currentJobs.Count >= MAX_JOB_COUNT)
                {
                    logger.Error($"Maximizing jobs limit, total count {this.currentJobs.Count}.");
                    throw new ActionException("Maximizing the number of jobs.");
                }
                var spy = this.OnCreateSpy(jobStartInfo);
                var context = this.OnCreateExecuteContext(jobStartInfo, spy);
                var task = executer.Execute(context);
                var jobinfo = new Job() { ExecutionId=context.ExecutionId, StartInfo = jobStartInfo, Spy = spy, Task = task };
                currentJobs[context.ExecutionId] = jobinfo;
                logger.Debug($"Add jobInfo in engine [{context.ExecutionId}]");
                task.ContinueWith((e) =>
                {
                    if (currentJobs.TryRemove(context.ExecutionId, out var job))
                    {
                        logger.Debug($"Remove jobInfo in engine [{context.ExecutionId}].");
                    }
                    else
                    {
                        logger.Warn($"Can not remove job info in engine [{context.ExecutionId}].");
                    }
                });
                return jobinfo;
            }
        }
        protected virtual IExecuteSpy OnCreateSpy(JobStartInfo jobStartInfo)
        {
            return new ExecuteSpy();
        }
        protected virtual IExecuteContext OnCreateExecuteContext(JobStartInfo jobStartInfo, IExecuteSpy spy)
        {
            var idGen = this.provider.GetRequiredService<IIdGenService>();
            var executionId = string.IsNullOrEmpty(jobStartInfo.ExecutionId) ? idGen.NewId() : jobStartInfo.ExecutionId;
            var parameters = new IActionParameters(jobStartInfo.Inputs, jobStartInfo.Context);
            return new ExecuteContext
            {
                ExecutionId = executionId,
                ActionRef = jobStartInfo.ActionRef,
                ParentExecutionId = null,
                RootExecutionId = executionId,
                ActionParameters = parameters,
                ExecuteSpy = spy,
                ExecutionDepth = 1,
                ActionRetryCount = jobStartInfo.RetryCount
            };
        }
        #endregion

        #region 其它
        void IDisposable.Dispose()
        {

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
