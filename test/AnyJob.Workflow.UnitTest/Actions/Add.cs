using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Actions
{
    public class Add : IAction
    {
        public double Num1 { get; set; }

        public double Num2 { get; set; }

        public object Run(IActionContext context)
        {
            return this.Num1 + this.Num2;
        }
    }
}
