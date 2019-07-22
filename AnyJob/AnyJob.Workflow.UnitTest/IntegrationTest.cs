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
            var inputs = new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            };
            var job = JobEngine.Start("nodepack.add", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
