using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Node
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
            NodeAction nodeAction = new NodeAction(option, "add.js");
            object res = nodeAction.Run(context);
            Assert.AreEqual(300, Convert.ToInt32(res));
        }
        [TestMethod]
        public void TestGlobalRef()
        {
            var option = this.CreateOption();
            var context = this.CreateActionContext(new Dictionary<string, object>()
            {
            });
            NodeAction nodeAction = new NodeAction(option, "global_ref.js");
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
            NodeAction nodeAction = new NodeAction(option, "pack_ref.js");
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
                Output = new ActionLogger(),
                Error = new ActionLogger(),
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
