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
            return Task.Run(() =>
            {
                return this.OnExecute(executeContext);
            });
        }

        protected virtual ExecuteResult OnExecute(IExecuteContext context)
        {
            try
            {
                var entry = this.OnResolveAction(context);
                var action = entry.CreateInstance(context.ActionParameters);
                var actionContext = this.OnCreateActionContext(entry.Meta, context);
                var result = action.Run(actionContext);
                return new ExecuteResult(result, true);
            }
            catch (Exception ex)
            {
                return new ExecuteResult()
                {
                    Error = ex,
                    IsSuccess = false
                };
            }
        }

        protected virtual IActionEntry OnResolveAction(IExecuteContext context)
        {
            var meta = this.actionResolverService.ResolveAction(context.ActionRef);
            if (meta == null)
            {
                throw new ActionException($"Can not resolve meta info from \"{context.ActionRef}\"");
            }
            return meta;
        }



        protected virtual IActionContext OnCreateActionContext(IActionMeta meta, IExecuteContext executeContext)
        {
            return new ActionContext()
            {
                Meta = meta,
                Parameters = executeContext.ActionParameters
            };
        }


        protected class ActionContext : IActionContext
        {
            private IValueProvider valueProvider;
            public ActionContext()
            {

            }
            public IActionParameters Parameters { get; set; }
            public IActionMeta Meta { get; set; }


        }
    }
}
