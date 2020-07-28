using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AnyJob.Runner.Workflow.IntegrationTest
{
    [TestClass]
    public class WorkflowActionTest
    {
        [TestMethod]
        public void ShouldSuccessWhenRunEmptyWorkflow()
        {
            var inputs = new Dictionary<string, object>();
            var job = JobEngine.Start("workflowpack.empty", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("empty", result.Result);
        }
        [TestMethod]
        public void ShouldSuccessWhenRunAdd100Workflow()
        {
            var inputs = new Dictionary<string, object>()
             {
                 { "num" , 500 },
             };
            var job = JobEngine.Start("workflowpack.add100", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(600L, result.Result);
        }
    }
}
