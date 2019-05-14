using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnyJob.Meta;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob.Intent
{
    public class IntentAction : IAction
    {
        public string ActionRef { get; set; }

        public string OutputMap { get; set; }

        public Dictionary<string, string> InputMaps { get; set; }

        public object Run(IActionContext context)
        {
            var expressionService = context.GetRequiredService<IExpressionService>();
            var executerService = context.GetRequiredService<IActionExecuterService>();

            var inputs = GetActualInputs(context, expressionService);
            var task=executerService.Execute(null);
            Task.WaitAll(task);
            if (task.Result.Error != null)
            {
                throw task.Result.Error;
            }
            return task.Result.Result;
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
    }

    public class ActionMeta : IActionMeta
    {
        public string Ref { get; set; }

        public string Kind { get; set; }

        public string Description { get; set; }

        public string DisplayFormat { get; set; }

        public bool Enabled { get; set; }

        public IEnumerable<IActionInputMeta> Inputs { get; set; }

        public IActionOutputMeta Output { get; set; }
    }
}
