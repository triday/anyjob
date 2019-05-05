using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob.Intent
{
    public class IntentAction : IAction
    {
        public string ActionRef { get; set; }

        public string OutputMap { get; set; }

        public Dictionary<string,string> InputMaps { get; set; }

        public object Run(IActionContext context)
        {
            var expressionService = context.GetRequiredService<IExpressionService>();
            var executerService = context.GetRequiredService<IActionExecuterService>();

            

            throw new NotImplementedException();
        }
    }
}
