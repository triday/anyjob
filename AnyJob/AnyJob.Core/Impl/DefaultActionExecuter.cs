using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private IActionNameResolveService actionNameResolveService;
        private Dictionary<string, IActionFactoryService> actionFactoryMap;
        public DefaultActionExecuter(
            ILogService logService,
            ITraceService traceService,
            IActionMetaService metaService,
            IActionRuntimeService runtimeService,
            IActionNameResolveService actionNameResolveService,
            IEnumerable<IActionFactoryService> actionFactories,
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.logService = logService;
            this.traceService = traceService;
            this.runtimeService = runtimeService;
            this.metaService = metaService;
            this.actionNameResolveService = actionNameResolveService;
            this.actionFactoryMap = actionFactories.ToDictionary(p => p.ActionKind, StringComparer.CurrentCultureIgnoreCase);
        }

        public Task<ExecuteResult> Execute(IExecuteContext executeContext)
        {
            this.OnTraceState(executeContext, ExecuteState.Ready);
            return Task.Run(() =>
            {
                executeContext.Token.ThrowIfCancellationRequested();
                this.OnTraceState(executeContext, ExecuteState.Running);
                var result = this.OnExecute(executeContext);
                if (result.IsSuccess)
                {
                    this.OnTraceState(executeContext, ExecuteState.Success, result);
                }
                else
                {
                    this.OnTraceState(executeContext, ExecuteState.Failure, result);
                }
                return result;
            }, executeContext.Token);
        }

        protected virtual ExecuteResult OnExecute(IExecuteContext executionContext)
        {
            try
            {
                //1 resolve action name
                var actionName = this.OnResolveActionName(executionContext.ActionFullName);
                //2 get runtime info
                var runtimeInfo = this.OnGetActionRuntime(actionName);
                //3 get meta info
                var metaInfo = this.OnGetActionMeta(actionName);
                //4 resolve action factory 
                var actionFactory = this.OnResolveActionFactory(metaInfo);
                //5 create action context
                var actionContext = this.OnCreateActionContext(executionContext, runtimeInfo, metaInfo);
                //6 check premission
                this.OnCheckPremission(actionContext);
                //7 valid inputs
                this.OnValidInputs(actionContext);
                //8 create action instance
                var actionInstance = this.OnCreateActionInstance(actionFactory, actionContext);
                //9 run action
                var result = OnRunAction(actionInstance, executionContext, actionContext);
                //10 valid output
                this.OnValidOutput(actionContext, result);

                return ExecuteResult.FromResult(result);
            }
            catch (Exception ex)
            {
                return ExecuteResult.FromError(ex);
            }
        }
        #region ExecuteSteps

        protected virtual IActionName OnResolveActionName(string actionFullName)
        {
            return this.actionNameResolveService.ResolverName(actionFullName);
        }

        protected virtual IActionRuntime OnGetActionRuntime(IActionName actionName)
        {
            return runtimeService.GetRunTime(actionName);
        }

        protected virtual IActionMeta OnGetActionMeta(IActionName actionName)
        {
            var actionMeta = this.metaService.GetActionMeta(actionName);
            if (actionMeta == null)
            {

            }
            return metaService.GetActionMeta(actionName);
        }

        protected virtual IActionFactoryService OnResolveActionFactory(IActionMeta actionMeta)
        {
            if (this.actionFactoryMap.TryGetValue(actionMeta.Kind, out var actionFactory))
            {
                return actionFactory;
            }
            else
            {
                return null;
            }
        }
        protected virtual IActionContext OnCreateActionContext(IExecuteContext executeContext, IActionRuntime actionRuntime, IActionMeta actionMeta)
        {
            return new ActionContext()
            {
                ExecutePath = executeContext.ExecutePath,
                Token = executeContext.Token,
                MetaInfo = actionMeta,
                RuntimeInfo = actionRuntime,
                ExecuteName = executeContext.ExecuteName,
                ServiceProvider = this.serviceProvider,
                Parameters = this.ConvertParameter(actionMeta, executeContext.ExecuteParameter),
                Logger = new ActionLogger()
            };
        }
        protected virtual IActionParameter ConvertParameter(IActionMeta actionMeta, IExecuteParameter executeParameter)
        {
            var runtimeArgs = new Dictionary<string, object>();
            if (actionMeta.Inputs != null)
            {
                foreach (var metaEntry in actionMeta.Inputs)
                {
                    if (metaEntry.Value.Default != null)
                    {
                        runtimeArgs.Add(metaEntry.Key, metaEntry.Value.Default);
                    }
                }
            }
            foreach (var inputEntry in executeParameter.Inputs)
            {
                runtimeArgs[inputEntry.Key] = inputEntry.Value;
            }

            return new ActionParameters()
            {
                Arguments = new ReadOnlyDictionary<string, object>(runtimeArgs),
                Context = executeParameter.Context,
                GlobalVars = executeParameter.GlobalVars,
                Vars = executeParameter.Vars,
            };

        }

        protected virtual void OnCheckPremission(IActionContext actionContext)
        {
            if (!actionContext.MetaInfo.Enabled)
            {

            }
        }
        protected virtual void OnValidInputs(IActionContext actionContext)
        {

        }

        protected virtual IAction OnCreateActionInstance(IActionFactoryService actionFactoryService, IActionContext actionContext)
        {
            var action = actionFactoryService.CreateAction(actionContext);
            if (action == null)
            {

            }
            return action;
        }

        protected virtual object OnRunAction(IAction action, IExecuteContext executeContext, IActionContext actionContext)
        {
            int loopCount = Math.Max(1, executeContext.ActionRetryCount);
            Exception error = null;
            for (int i = 0; i < loopCount && !executeContext.Token.IsCancellationRequested; i++)
            {
                try
                {
                    return action.Run(actionContext);
                }
                catch (Exception ex)
                {
                    logService.Warn("Error in execute action [{0}] {1} time(s).\n{2}", executeContext.ActionFullName, i, ex);
                    error = ex;
                }
            }
            throw error;
        }

        protected virtual void OnValidOutput(IActionContext actionContext, object result)
        {

        }


        #endregion



        protected virtual void OnTraceState(IExecuteContext context, ExecuteState state, ExecuteResult result = null)
        {
            traceService.TraceState(context, state, result);
        }

    }
}
