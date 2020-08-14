using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultActionExecuter : IActionExecuterService
    {
        private IServiceProvider serviceProvider;
        private IActionRuntimeService runtimeService;
        private IActionMetaService metaService;
        private ILogger logger;
        private ITraceService traceService;
        private IActionNameResolveService actionNameResolveService;
        private IPreparePackService preparePackService;
        private IDictionary<string, IActionFactoryService> actionFactoryMap;

        public DefaultActionExecuter(
            ILogger<DefaultActionExecuter> logger,
            ITraceService traceService,
            IActionMetaService metaService,
            IActionRuntimeService runtimeService,
            IPreparePackService preparePackService,
            IActionNameResolveService actionNameResolveService,
            IDictionary<string, IActionFactoryService> actionFactoryMap,
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.traceService = traceService;
            this.runtimeService = runtimeService;
            this.metaService = metaService;
            this.actionNameResolveService = actionNameResolveService;
            this.preparePackService = preparePackService;
            this.actionFactoryMap = actionFactoryMap;
        }

        public Task<ExecuteResult> Execute(IExecuteContext executeContext)
        {
            _ = executeContext ?? throw new ArgumentNullException(nameof(executeContext));
            var traceInfo = new TraceInfo()
            {
                ExecuteContext = executeContext
            };
            this.OnTraceState(traceInfo, ExecuteState.Ready);

            return Task.Run(() =>
            {
                executeContext.Token.ThrowIfCancellationRequested();
                this.OnTraceState(traceInfo, ExecuteState.Running);
                var result = this.OnExecute(executeContext, traceInfo);
                if (result.IsSuccess)
                {
                    this.OnTraceState(traceInfo, ExecuteState.Success, result);
                }
                else
                {
                    this.OnTraceState(traceInfo, ExecuteState.Failure, result);
                }
                return result;
            }, executeContext.Token);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        protected virtual ExecuteResult OnExecute(IExecuteContext executionContext, TraceInfo traceInfo)
        {
            _ = executionContext ?? throw new ArgumentNullException(nameof(executionContext));
            _ = traceInfo ?? throw new ArgumentNullException(nameof(traceInfo));
            IActionContext actionContext = null;
            try
            {
                //1 resolve action name
                var actionName = this.OnResolveActionName(executionContext.ActionFullName);
                traceInfo.ActionName = actionName;
                //2 get runtime info
                var runtimeInfo = this.OnGetActionRuntime(actionName);
                traceInfo.ActionRuntime = runtimeInfo;
                //3 prepare package
                this.OnPrepareAction(actionName, runtimeInfo);
                //4 get meta info
                var metaInfo = this.OnGetActionMeta(actionName);
                traceInfo.ActionMeta = metaInfo;
                //5 resolve action factory 
                var actionFactory = this.OnResolveActionFactory(metaInfo);
                //6 create action context
                actionContext = this.OnCreateActionContext(executionContext, actionName, runtimeInfo, metaInfo);

                //7 check premission
                this.OnCheckPremission(actionContext);
                //8 valid inputs
                this.OnValidInputs(actionContext);
                //9 create action instance
                var actionInstance = this.OnCreateActionInstance(actionFactory, actionContext);
                //10 run action
                var result = OnRunAction(actionInstance, executionContext, actionContext);
                //11 valid output
                this.OnValidOutput(actionContext, result);
                return ExecuteResult.FromResult(result, actionContext.Logger.ToString());
            }
            catch (Exception ex)
            {
                return ExecuteResult.FromError(ex, actionContext?.Logger?.ToString());
            }
        }
        #region ExecuteSteps

        protected virtual IActionName OnResolveActionName(string actionFullName)
        {
            IActionName actionName = this.actionNameResolveService.ResolverName(actionFullName);
            if (actionName == null)
            {
                throw Errors.ResolveNullActionName(actionFullName);
            }
            return actionName;
        }


        protected virtual IActionRuntime OnGetActionRuntime(IActionName actionName)
        {
            _ = actionName ?? throw new ArgumentNullException(nameof(actionName));
            var actionRuntime = runtimeService.GetRunTime(actionName);
            if (actionRuntime == null)
            {
                throw Errors.GetNullRuntimeInfo(actionName.ToString());
            }
            return actionRuntime;

        }
        protected virtual void OnPrepareAction(IActionName actionName, IActionRuntime actionRuntime)
        {
            this.preparePackService.PreparePack(actionName, actionRuntime);
        }
        protected virtual IActionMeta OnGetActionMeta(IActionName actionName)
        {
            _ = actionName ?? throw new ArgumentNullException(nameof(actionName));
            var actionMeta = this.metaService.GetActionMeta(actionName);
            if (actionMeta == null)
            {
                throw Errors.GetNullMetaInfo(actionName.ToString());
            }
            return metaService.GetActionMeta(actionName);
        }

        protected virtual IActionFactoryService OnResolveActionFactory(IActionMeta actionMeta)
        {
            _ = actionMeta ?? throw new ArgumentNullException(nameof(actionMeta));
            if (this.actionFactoryMap.TryGetValue(actionMeta.Kind, out var actionFactory))
            {
                return actionFactory;
            }
            else
            {
                throw Errors.CannotGetActionFactory(actionMeta.Kind);
            }
        }
        protected virtual IActionContext OnCreateActionContext(IExecuteContext executeContext, IActionName actionName, IActionRuntime actionRuntime, IActionMeta actionMeta)
        {
            _ = executeContext ?? throw new ArgumentNullException(nameof(executeContext));
            return new ActionContext()
            {
                ExecutePath = executeContext.ExecutePath,
                Token = executeContext.Token,
                MetaInfo = actionMeta,
                RuntimeInfo = actionRuntime,
                ExecuteName = executeContext.ExecuteName,
                ServiceProvider = this.serviceProvider,
                Parameters = this.ConvertParameter(actionMeta, executeContext.ExecuteParameter),
                Logger = new ActionLogger(),
                Name = actionName
            };
        }
        protected virtual IActionParameter ConvertParameter(IActionMeta actionMeta, IExecuteParameter executeParameter)
        {
            _ = actionMeta ?? throw new ArgumentNullException(nameof(actionMeta));
            _ = executeParameter ?? throw new ArgumentNullException(nameof(executeParameter));
            var runtimeArgs = new Dictionary<string, object>();
            if (actionMeta.Inputs != null)
            {
                foreach (var metaEntry in actionMeta.Inputs)
                {
                    var defaultValue = metaEntry.Value.DefaultValue;
                    if (defaultValue != null)
                    {
                        runtimeArgs.Add(metaEntry.Key, defaultValue);
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
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            //if (!actionContext.MetaInfo.Enabled)
            //{
            //    throw Errors.ActionIsDisabled(actionContext.Name.ToString());
            //}
        }
        protected virtual void OnValidInputs(IActionContext actionContext)
        {

        }

        protected virtual IAction OnCreateActionInstance(IActionFactoryService actionFactoryService, IActionContext actionContext)
        {
            _ = actionFactoryService ?? throw new ArgumentNullException(nameof(actionFactoryService));
            var action = actionFactoryService.CreateAction(actionContext);
            if (action == null)
            {

            }
            return action;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        protected virtual object OnRunAction(IAction action, IExecuteContext executeContext, IActionContext actionContext)
        {
            _ = executeContext ?? throw new ArgumentNullException(nameof(executeContext));
            _ = action ?? throw new ArgumentNullException(nameof(action));
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
                    this.logger.LogWarning("Error in execute action [{0}] {1} time(s).\n{2}", executeContext.ActionFullName, i, ex);
                    error = ex;
                }
            }
            throw error;
        }

        protected virtual void OnValidOutput(IActionContext actionContext, object result)
        {

        }


        #endregion



        protected virtual void OnTraceState(TraceInfo traceInfo, ExecuteState state, ExecuteResult result = null)
        {
            _ = traceInfo ?? throw new ArgumentNullException(nameof(traceInfo));
            traceInfo.State = state;
            traceInfo.Result = result;
            traceService.TraceState(traceInfo);
        }


    }
}
