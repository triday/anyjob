using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyJob.Runner.Intent
{
    public class IntentAction : IAction
    {
        public string ActionRef { get; set; }

        public string OutputMap { get; set; }

        public Dictionary<string, string> InputMaps { get; set; }

        public virtual object Run(IActionContext context)
        {
            var executerService = context.GetRequiredService<IActionExecuterService>();
            var execContext = this.OnCreateExecuteContext(context);
            var task = executerService.Execute(execContext);
            Task.WaitAll(task);
            if (task.Result.Error != null)
            {
                throw task.Result.Error;
            }
            return this.OnHandlerResult(task.Result.Result);
        }

        protected virtual object OnHandlerResult(object innerResult)
        {
            return innerResult;
        }
        private Dictionary<string, object> GetActualInputs(IActionContext context, IExpressionService expressionService)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            if (this.InputMaps != null)
            {
                foreach (var kv in this.InputMaps)
                {
                    object val = expressionService.Exec(kv.Value, null);
                    args[kv.Key] = val;
                }
            }
            return args;
        }

        protected virtual IExecuteContext OnCreateExecuteContext(IActionContext actionContext)
        {
            return null;
            //var idgenService = actionContext.GetRequiredService<IIdGenService>();
            //var newid = idgenService.NewId();
            //return new ExecuteContext()
            //{
            //    ActionName = new ActionName(this.ActionRef),
            //    Token = actionContext.Token,
            //    ExecutePath = actionContext.ExecutePath.NewSubPath(newid),
            //    ActionRetryCount = 1,
            //    ActionParameters = null,
            //};
        }

        protected virtual ActionParameters OnCreateParams(IActionContext actionContext)
        {
            return null;
        }
    }


}
