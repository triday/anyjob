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
        private IMetaResolverService metaResolverService;
        private IServiceProvider serviceProvider;
        public DefaultActionExecuter(IMetaResolverService metaResolverService,IActionResolverService actionResolverService,IServiceProvider serviceProvider)
        {
            this.metaResolverService = metaResolverService;
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
                var meta = this.OnResolveMeta(context);
                var action = this.OnResolveAction(meta,context);
                var actionContext = this.OnCreateActionContext(meta, context);
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

        protected virtual IActionMeta OnResolveMeta(IExecuteContext context)
        {
            var meta = this.metaResolverService.ResolveMeta(context.ActionRef);
            if (meta == null)
            {
                throw new ActionException($"Can not resolve meta info from \"{context.ActionRef}\"");
            }
            return meta;
        }

        protected virtual IAction OnResolveAction(IActionMeta meta, IExecuteContext context)
        {
            var action = this.actionResolverService.ResolveAction(meta, context.ActionParameters);
            if (action == null)
            {
                throw new ActionException($"Can not resolve action from \"{meta.Ref}\"");
            }
            return action;
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
