using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AnyJob.Runner.App.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Runner.App.UnitTest
{
    [TestClass]
    public class AppActionTest
    {
        private bool IsWindows = (int)(System.Environment.OSVersion.Platform) < 4;
        [TestMethod]
        public void TestPingWithCount()
        {
            AppInfo appInfo = new AppInfo()
            {
                Command = IsWindows ? "ping ${host} -n ${count}" : "ping ${host} -c ${count}"

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
                Command = IsWindows ? "cmd /c echo ${text}" : "sh -c \"echo ${text}\""
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
                    WorkingDirectory = System.Environment.CurrentDirectory,
                    OSPlatForm = OSPlatform.Windows
                }
            };
        }


    }
}
