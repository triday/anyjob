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
            }, executeContext.ExecuteSpy.Token);
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
                    Result = result,
                };
            }
            catch (OperationCanceledException ex)
            {
                return new ExecuteResult()
                {
                    Error = ex,
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
                    logService.Warn("Error in execute action [{0}] {1} time(s).\n{2}", context.MetaInfo.Ref, i, ex);
                    error = ex;
                }
            }
            throw error;
        }

        protected virtual IActionContext OnCreateActionContext(IActionMeta meta, IExecuteContext executeContext)
        {
            return new ActionContext(this.serviceProvider)
            {
                Token = executeContext.ExecuteSpy.Token,
                MetaInfo = meta,
                Parameters = executeContext.ActionParameters
            };
        }


        protected virtual void OnSafeTraceState(IExecuteContext context, ExecuteState state, ExecuteResult result = null)
        {
            try
            {
                traceService.TraceState(context, state, result);
            }
            catch (Exception ex)
            {
                logService.Error("Trace state error: {0}.", ex);
            }
        }

    }
}
