using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Runner.Node
{
    [TestClass]
    public class NodeActionTest : YS.Knife.Hosting.KnifeHost
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
            NodeAction nodeAction = new NodeAction(option, new NodeEntryInfo { Module = "add.js", Method = "run" });
            object res = nodeAction.Run(context);
            Assert.AreEqual(300, Convert.ToInt32(res));
        }

        private NodeOptions CreateOption()
        {
            return new NodeOptions()
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
                ServiceProvider = this,
                Parameters = new ActionParameters()
                {
                    Arguments = inputs,
                },
                RuntimeInfo = new ActionRuntime()
                {
                    WorkingDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "packs/nodepack"),
                }


            };
        }
    }
}
