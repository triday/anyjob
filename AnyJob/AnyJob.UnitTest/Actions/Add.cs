using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.UnitTest.Actions
{
    
    public class Add : IAction
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public object Run(IActionContext context)
        {
            return Num1 + Num2;
        }
    }
}
