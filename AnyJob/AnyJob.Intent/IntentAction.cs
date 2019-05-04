using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Intent
{
    public class IntentAction : IAction
    {
        public string ActionRef { get; set; }

        public string OutputMap { get; set; }

        public Dictionary<string,string> InputMaps { get; set; }

        public object Run(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
