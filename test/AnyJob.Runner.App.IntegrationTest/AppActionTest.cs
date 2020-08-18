using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Runner.App.IntegrationTest
{
    [TestClass]
    public class AppActionTest
    {
        [TestMethod]
        public void ShouldInvokeDotnetVersionSuccess()
        {
            var inputs = new Dictionary<string, object>()
            {

            };
            var job = JobEngine.Start("apppack.dotversion", inputs);
            dynamic result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, result.Result.ExitCode);
        }
        [TestMethod]
        public void ShouldInvokeHelloSuccess()
        {
            var inputs = new Dictionary<string, object>()
            {
                ["name"] = "zhangsan"
            };
            var job = JobEngine.Start("apppack.hello", inputs);
            dynamic result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, result.Result.ExitCode);
        }
    }
}
