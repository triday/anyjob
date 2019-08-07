using AnyJob.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Python
{
    [TestClass]
    public class PythonActionTest : JobTestBase
    {
        [TestMethod]
        public void TestAdd()
        {
            var option = this.CreateOption();
            var context = this.CreateActionContext(new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            });
            PythonAction nodeAction = new PythonAction(option, "add.py");
            object res = nodeAction.Run(context);
            Assert.AreEqual(300, Convert.ToInt32(res));
        }

        private PythonOption CreateOption()
        {
            return new PythonOption()
            {
            };
        }

        private IActionContext CreateActionContext(IDictionary<string, object> inputs)
        {
            return new ActionContext()
            {
                ExecuteName = string.Empty,
                ExecutePath = ExecutePath.RootPath(Guid.NewGuid().ToString()),
                Logger = new ActionLogger(),
                ServiceProvider = ServiceCenter.CurrentProvider,
                Parameters = new ActionParameters()
                {
                    Arguments = inputs,
                },
                RuntimeInfo = new ActionRuntime()
                {
                    WorkingDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "packs/pythonpack"),
                }


            };
        }
    }

}
