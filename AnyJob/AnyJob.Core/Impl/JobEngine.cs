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
        private IServiceCollection services = new ServiceCollection();
        private IServiceProvider provider;
        public void ConfigServices(Action<IServiceCollection> config)
        {
            if (config != null)
            {
                config(this.services);
            }
            this.provider = services.BuildServiceProvider();
        }

        public void ConfigServices(params Assembly[] assemblies)
        {
            ConfigServices((services) => services.ConfigAssemblyServices(assemblies));
        }
        public void ConfigServices()
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
            
            var executer = this.GetRequiredService<IActionExecuterService>();
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
            var idGen = this.GetRequiredService<IIdGenService>();
            var executionId = String.IsNullOrEmpty(jobStartInfo.ExecutionId) ? idGen.NewId() : jobStartInfo.ExecutionId;
            var parameters = new ActionParameters(jobStartInfo.Inputs, jobStartInfo.Context);
            var cancelSource = jobStartInfo.TimeoutSeconds > 0 ? new CancellationTokenSource(jobStartInfo.TimeoutSeconds * 1000) : new CancellationTokenSource();
            return new ExecuteContext(this)
            {
                ExecutionId = executionId,
                ActionRef = jobStartInfo.ActionRef,
                ParentExecutionId = null,
                RootExecutionId = executionId,
                ActionParameters = parameters,
                CancelTokenSource = cancelSource,
            };
        }

        object IServiceProvider.GetService(Type serviceType)
        {
            return this.provider.GetService(serviceType);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~JobEngine() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
