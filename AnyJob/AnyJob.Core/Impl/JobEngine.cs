using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Impl
{
    public class JobEngine :  IJobEngine
    {
        public JobEngine()
        {
            this.provider = services.BuildServiceProvider();
        }
        private ServiceCollection services = new ServiceCollection();
        private IServiceProvider provider;
        public void ConfigServices(Action<IServiceCollection> config)
        {
            if (config != null)
            {
                config(this.services);
            }
            this.provider = services.BuildServiceProvider();
        }

        public void ConfigServices(Assembly[] assemblies, Func<Type, Type, bool> filter = null)
        {
            ConfigServices((services) => services.ConfigAssemblyServices(assemblies));
        }
        public void ConfigServices(Func<Type,Type,bool> filter=null)
        {
            ConfigServices(AppDomain.CurrentDomain.GetAssemblies());
        }

        public bool Cancel(string jobId)
        {
            return false;
        }

        public Job Start(JobStartInfo jobStartInfo)
        {
            if (jobStartInfo == null)
            {
                throw new ArgumentNullException(nameof(jobStartInfo));
            }
            var executer = this.provider.GetRequiredService<IActionExecuterService>();
            var context = this.OnCreateExecuteContext(jobStartInfo);
            return new Job()
            {
                ExecutionId = context.ExecutionId,
                ActionRef = context.ActionRef,
                ActionParameters = context.ActionParameters,
                Task = executer.Execute(context)
            };
        }

        protected virtual IExecuteContext OnCreateExecuteContext(JobStartInfo jobStartInfo)
        {
            var idGen = this.provider.GetRequiredService<IIdGenService>();
            var executionId = string.IsNullOrEmpty(jobStartInfo.ExecutionId) ? idGen.NewId() : jobStartInfo.ExecutionId;
            var parameters = new IActionParameters(jobStartInfo.Inputs, jobStartInfo.Context);
            var cancelSource = jobStartInfo.TimeoutSeconds > 0 ? new CancellationTokenSource(jobStartInfo.TimeoutSeconds * 1000) : new CancellationTokenSource();
            return new ExecuteContext
            {
                ExecutionId = executionId,
                ActionRef = jobStartInfo.ActionRef,
                ParentExecutionId = null,
                RootExecutionId = executionId,
                ActionParameters = parameters,
                CancelTokenSource = cancelSource,
            };
        }

        public void Dispose()
        {
        }

        public class ExecuteContext : IExecuteContext
        {
            public string ExecutionId { get; set; }

            public string ParentExecutionId { get; set; }

            public string RootExecutionId { get; set; }

            public string ActionRef { get; set; }

            public IActionParameters ActionParameters { get; set; }

            public CancellationTokenSource CancelTokenSource { get; set; }

        }
    }
}
