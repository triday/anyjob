using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.UnitTest.Actions
{
    [Action("test.add", Description = "Test add", DisplayFormat = "${num1}+ ${num2}")]
    public class Add : IAction
    {
        [ActionInput("num1", Description = "参数1", Default = 1, IsRequired = true)]
        public int Num1 { get; set; }
        [ActionInput("num2", Description = "参数2", Default = 2, IsRequired = true)]
        public int Num2 { get; set; }
        public object Run(IActionContext context)
        {
            return Num1 + Num2;
        }
    }
}
