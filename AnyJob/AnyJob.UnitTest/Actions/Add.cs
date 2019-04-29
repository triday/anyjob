using AnyJob.Assembly.Meta;
using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.UnitTest.Actions
{
    [ActionOutput(typeof(int))]
    [Action("test.add", Description = "Test add", DisplayFormat = "${num1}+ ${num2}")]
    public class Add : IAction
    {
        [ActionInput( Description = "参数1", Default = 1, IsRequired = true)]
        public int Num1 { get; set; }
        [ActionInput( Description = "参数2", Default = 2, IsRequired = true)]
        public int Num2 { get; set; }


        public object Run(IActionContext context)
        {
            return Num1 + Num2;
        }
    }
}
