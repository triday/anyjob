using AnyJob.App.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnyJob.App.UnitTest
{
    [TestClass]
    public class AppActionTest
    {
        [TestMethod]
        public void TestPingWithCount()
        {
            AppInfo appInfo = new AppInfo()
            {
                Command = "ping ${host} -n ${count}"
            };
            AppOption appOption = new AppOption();
            AppAction appAction = new AppAction(appInfo, appOption);
            var inputs = new Dictionary<string, object>()
            {
                {"host","127.0.0.1"},
                {"count",1}
            };
            var context = this.CreateActionContext(inputs);
            object res = appAction.Run(context);
            Assert.IsNotNull(res);
        }
        [TestMethod]
        public void TestEchoAbc()
        {
            AppInfo appInfo = new AppInfo()
            {
                Command = "cmd /c echo ${text}"
            };
            AppOption appOption = new AppOption();
            AppAction appAction = new AppAction(appInfo, appOption);
            var inputs = new Dictionary<string, object>()
            {
                {"text","abc"}
            };
            var context = this.CreateActionContext(inputs);
            object res = appAction.Run(context);
            Assert.IsNotNull(res);
            Assert.AreEqual("abc", System.Convert.ToString(res).Trim());
        }

        private IActionContext CreateActionContext(IDictionary<string, object> inputs)
        {
            return new ActionContext()
            {
                Parameters = new ActionParameters()
                {
                    Arguments = inputs
                },
                Output = new ActionLogger(),
                Error = new ActionLogger(),
                RuntimeInfo = new ActionRuntime()
                {
                    WorkingDirectory = System.Environment.CurrentDirectory

                }
            };
        }


    }
}