using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Runner.App.IntegrationTest
{
    [TestClass]
    public class AppActionTest
    {
        private bool IsWindows = (int)(System.Environment.OSVersion.Platform) < 4;
        [TestMethod]
        public void ShouldInvokeDotnetVersionSuccess()
        {
            var inputs = new Dictionary<string, object>()
            {

            };
            var job = JobEngine.Start("apppack.dotversion", inputs);
            ExecuteResult result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Result is AppResult);
            var appResult = result.Result as AppResult;
            Assert.AreEqual(0, appResult.ExitCode);
        }
        [TestMethod]
        public void ShouldInvokeHelloSuccess()
        {
            var inputs = new Dictionary<string, object>()
            {
                ["name"] = "zhangsan"
            };
            var job = JobEngine.Start(IsWindows ? "apppack.hello_win" : "apppack.hello", inputs);
            dynamic result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Result is AppResult);
            var appResult = result.Result as AppResult;
            Assert.AreEqual(0, appResult.ExitCode);
            Assert.IsTrue(appResult.Output.Contains("hello,zhangsan"));
        }
    }
}
