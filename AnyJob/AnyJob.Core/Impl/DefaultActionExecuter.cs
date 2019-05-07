using AnyJob.Meta;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionExecuterService))]
    public class DefaultActionExecuter : IActionExecuterService
    {
        private IActionResolverService actionResolverService;
        private IServiceProvider serviceProvider;
        public DefaultActionExecuter(IActionResolverService actionResolverService,IServiceProvider serviceProvider)
        {
            this.actionResolverService = actionResolverService;
            this.serviceProvider = serviceProvider;
        }

        public Task<ExecuteResult> Execute(IExecuteContext executeContext)
        {
            this.OnUpdateState(executeContext, ExecuteState.Schedule);
            return Task.Run(() =>
            {
                this.OnUpdateState(executeContext, ExecuteState.Running);
                var result= this.OnExecute(executeContext);

                if (result.IsSuccess)
                {
                    this.OnUpdateState(executeContext, ExecuteState.Success);
                }
                else
                {
                    this.OnUpdateState(executeContext, ExecuteState.Failure);
                }

                return result;

            });
        }

        protected virtual ExecuteResult OnExecute(IExecuteContext context)
        {
            try
            {
                var entry = this.OnResolveAction(context);
                var action = entry.CreateInstance(context.ActionParameters);
                var actionContext = this.OnCreateActionContext(entry.MetaInfo, context);
                var result = action.Run(actionContext);
                return new ExecuteResult()
                {
                     Result=result,
                };
            }
            catch (Exception ex)
            {
                return new ExecuteResult()
                {
                    Error = ex
                };
            }
        }

        protected virtual IActionEntry OnResolveAction(IExecuteContext context)
        {
            var entry = this.actionResolverService.ResolveAction(context.ActionRef);
            if (entry == null)
            {
                throw new ActionException($"Can not resolve entry info from \"{context.ActionRef}\"");
            }
            return entry;
        }



        protected virtual IActionContext OnCreateActionContext(IActionMeta meta, IExecuteContext executeContext)
        {
            return new ActionContext(this.serviceProvider)
            {
                MetaInfo = meta,
                Parameters = executeContext.ActionParameters
            };
        }


        protected virtual void OnUpdateState(IExecuteContext context, ExecuteState state)
        {
            var traceService = this.serviceProvider.GetService<ITraceService>();
            if (traceService != null)
            {

            }
        }

    }
}
