using AnyJob.Meta;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionExecuterService))]
    public class DefaultActionExecuter : IActionExecuterService
    {
        private IServiceProvider serviceProvider;
        private IActionRuntimeService runtimeService;
        private IActionMetaService metaService;
        private ILogService logService;
        private ITimeService timeService;
        private ITraceService traceService;
        private Dictionary<string, IActionDefinationFactory> definationFactories;
        public DefaultActionExecuter(
            ILogService logService,
            ITimeService timeService,
            ITraceService traceService,
            IActionMetaService metaService,
            IActionRuntimeService runtimeService,
            IEnumerable<IActionDefinationFactory> definationFactories,
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.logService = logService;
            this.timeService = timeService;
            this.traceService = traceService;
            this.runtimeService = runtimeService;
            this.metaService = metaService;
            this.definationFactories = definationFactories.ToDictionary(p => p.ActionKind, StringComparer.CurrentCultureIgnoreCase);
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

        protected virtual ExecuteResult OnExecute(IExecuteContext executionContext)
        {
            try
            {
                //1 get runtime info
                var runtimeInfo = this.OnGetActionRuntime(executionContext);
                //2 get meta info
                var metaInfo = this.OnGetActionMeta(executionContext, runtimeInfo);
                //3 get action defination
                var actionDefination = this.OnGetActionDefination(executionContext,runtimeInfo,metaInfo);
                //4 create action instance
                var action = actionDefination.CreateInstance(executionContext.ActionParameters);
                //5 create action context
                var actionContext = this.OnCreateActionContext(executionContext,runtimeInfo,metaInfo);
                //6 run action
                var result = this.OnRunAction(action, executionContext, actionContext);

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
        #region ExecuteSteps
        protected virtual IActionRuntime OnGetActionRuntime(IExecuteContext executeContext)
        {
            return runtimeService.GetRunTime(executeContext.ActionName);
        }
        protected virtual IActionMeta OnGetActionMeta(IExecuteContext executeContext,IActionRuntime actionRuntime)
        {
            return metaService.GetActionMeta(actionRuntime, executeContext.ActionName);
        }
        protected virtual IActionDefination OnGetActionDefination(IExecuteContext context,IActionRuntime actionRuntime,IActionMeta actionMeta)
        {
            var definationFactory = this.definationFactories[actionMeta.ActionKind];
            var actionDefination = definationFactory.GetActionDefination(actionRuntime, actionMeta);
            if (actionDefination == null)
            {
                throw new ActionException($"Can not resolve desc info from \"{context.ActionName.FullName}\"");
            }
            return actionDefination;
        }
        protected virtual IAction OnCreateAction(IExecuteContext executionContext, IActionDefination actionDefination)
        {
            return actionDefination.CreateInstance(executionContext.ActionParameters);
        }
        protected virtual IActionContext OnCreateActionContext(IExecuteContext executeContext, IActionRuntime actionRuntime, IActionMeta actionMeta)
        {
            if (executeContext == null)
            {
                throw new ArgumentNullException(nameof(executeContext));
            }
            return new ActionContext(this.serviceProvider)
            {
                ExecutePath = executeContext.ExecutePath,
                Token = executeContext.Token,
                MetaInfo = actionMeta,
                RuntimeInfo= actionRuntime,
                Parameters = executeContext.ActionParameters
            };
        }
        protected virtual object OnRunAction(IAction action,IExecuteContext executeContext, IActionContext actionContext)
        {
            int loopCount = Math.Min(1, executeContext.ActionRetryCount);
            Exception error = null;
            for (int i = 0; i < loopCount && !executeContext.Token.IsCancellationRequested; i++)
            {
                try
                {
                    return action.Run(actionContext);
                }
                catch (Exception ex)
                {
                    logService.Warn("Error in execute action [{0}] {1} time(s).\n{2}", executeContext.ActionName.FullName, i, ex);
                    error = ex;
                }
            }
            throw error;
        }
        #endregion



        protected virtual void OnSafeTraceState(IExecuteContext context, ExecuteState state, ExecuteResult result = null)
        {
            traceService.TraceState(context, state, result);
        }

    }
}
