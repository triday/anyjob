using AnyJob.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnyJob.Workflow
{
    [TestClass]
    public class IntegrationTest : JobTestBase
    {
        [TestMethod]
        public void TestEmpty()
        {
            var inputs = new Dictionary<string, object>();
            var job = JobEngine.Start("workflow.empty", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("empty", result.Result);
        }
    }
}
