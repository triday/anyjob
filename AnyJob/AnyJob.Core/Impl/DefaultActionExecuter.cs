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
        public Task<ExecuteResult> Execute(IExecuteContext executeContext)
        {
            return null;
        }

        protected virtual ExecuteResult OnExecute(IExecuteContext context)
        {
            var entry = this.OnResolveEntry(context);

            try
            {
                var result = entry.Action.Run(null);
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
            var resolver = context.GetRequiredService<IActionResolverService>();
            var actionEntry = resolver.ResolveAction(context.ActionRef);
            if (actionEntry == null)
            {
                throw new ActionException($"Can not find action \"{context.ActionRef}\".");
            }
            else
            {
                return actionEntry;
            }
        }
    }
}
