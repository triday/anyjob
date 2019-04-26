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
        private IActionFactoryService factory;
        public DefaultActionExecuter(IActionFactoryService factory)
        {
            this.factory = factory;
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
                var entry = this.OnResolveEntry(context);
                var actionContext = this.OnCreateActionContext(entry, context);
                var result = entry.Action.Run(actionContext);
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

        protected virtual ActionEntry OnResolveEntry(IExecuteContext context)
        {
            var actionEntry = factory.Get(context.ActionRef);
            if (actionEntry == null)
            {
                throw new ActionException($"Can not find action \"{context.ActionRef}\".");
            }
            else
            {
                return actionEntry;
            }
        }

        protected virtual IActionContext OnCreateActionContext(ActionEntry entry, IExecuteContext executeContext)
        {
            return new ActionContext()
            {
                Meta = entry.Meta,
                Parameters = executeContext.ActionParameters
            };
        }
    }
}
