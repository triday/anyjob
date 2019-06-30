using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnyJob.Node
{
    [TestClass]
    public class NodeActionTest:JobTestBase
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
            NodeAction nodeAction = new NodeAction(option, "./node_actions/add.js");
            object res= nodeAction.Run(context);
            Assert.AreEqual(300, Convert.ToInt32(res));
        }
        [TestMethod]
        public void TestGlobalRef() {
            var option = this.CreateOption();
            var context = this.CreateActionContext(new Dictionary<string, object>()
            {
            });
            NodeAction nodeAction = new NodeAction(option, "./node_actions/global_ref.js");
            object res = nodeAction.Run(context);
            Assert.AreEqual("ok", res);
        }
        [TestMethod]
        public void TestPackRef()
        {
            var option = this.CreateOption();
            var context = this.CreateActionContext(new Dictionary<string, object>()
            {
            });
            NodeAction nodeAction = new NodeAction(option, "./node_actions/pack_ref.js");
            object res = nodeAction.Run(context);
            Assert.AreEqual("ok", res);
        }
        private NodeOption CreateOption()
        {
            return new NodeOption()
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
                Parameters = new ActionParameters(inputs),
                RuntimeInfo = new ActionRuntime()
                {
                     WorkingDirectory=Environment.CurrentDirectory,
                }


            };
        }
    }
}