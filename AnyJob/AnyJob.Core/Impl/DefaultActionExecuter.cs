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
        private ILogService logService;
        private ITimeService timeService;
        private ITraceService traceService;

        public DefaultActionExecuter(IActionResolverService actionResolverService, ILogService logService, ITimeService timeService, ITraceService traceService, IServiceProvider serviceProvider)
        {
            this.actionResolverService = actionResolverService;
            this.serviceProvider = serviceProvider;
            this.logService = logService;
            this.timeService = timeService;
            this.traceService = traceService;
        }

        public Task<ExecuteResult> Execute(IExecuteContext executeContext)
        {
            this.OnSafeTraceState(executeContext, ExecuteState.Ready);
            return Task.Run(() =>
            {
                executeContext.Token.ThrowIfCancellationRequested();
                this.OnSafeTraceState(executeContext, ExecuteState.Running);
                var result = this.OnExecute(executeContext);
                if (result.IsSuccess)
                {
                    this.OnSafeTraceState(executeContext, ExecuteState.Success, result);
                }
                else
                {
                    this.OnSafeTraceState(executeContext, ExecuteState.Failure, result);
                }
                return result;
            }, executeContext.Token);
        }

        protected virtual ExecuteResult OnExecute(IExecuteContext context)
        {
            try
            {
                var entry = this.OnResolveAction(context);
                var action = entry.CreateInstance(context.ActionParameters);
                var actionContext = this.OnCreateActionContext(entry, context);
                var result = action.Run(actionContext);
                return new ExecuteResult()
                {
                    Result = result,
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

        protected virtual IActionDesc OnResolveAction(IExecuteContext context)
        {
            var desc = this.actionResolverService.ResolveActionDesc(context.ActionName);
            if (desc == null)
            {
                throw new ActionException($"Can not resolve desc info from \"{context.ActionName.FullName}\"");
            }
            return desc;
        }

        protected virtual object OnRunAction(IAction action, IActionContext context, int retryCount)
        {
            int loopCount = Math.Min(1, retryCount);
            Exception error = null;
            for (int i = 0; i < loopCount; i++)
            {
                try
                {
                    return action.Run(context);
                }
                catch (Exception ex)
                {
                    logService.Warn("Error in execute action [{0}] {1} time(s).\n{2}", context?.EntryInfo?.ActionName?.FullName, i, ex);
                    error = ex;
                }
            }
            throw error;
        }

        protected virtual IActionContext OnCreateActionContext(IActionMeta meta, IExecuteContext executeContext)
        {
            return new ActionContext(this.serviceProvider)
            {
                ExecutePath = executeContext.ExecutePath,
                Token = executeContext.Token,
                MetaInfo = meta,
                Parameters = executeContext.ActionParameters
            };
        }


        protected virtual void OnSafeTraceState(IExecuteContext context, ExecuteState state, ExecuteResult result = null)
        {
            traceService.TraceState(context, state, result);
        }

    }
}
