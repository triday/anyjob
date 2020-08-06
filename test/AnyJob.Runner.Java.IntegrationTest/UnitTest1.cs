using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AnyJob.Runner.Java.IntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShouldInvokeSuccessWhenAddTwoArgument()
        {
            var inputs = new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            };
            var job = JobEngine.Start("javapack.add", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(300L, result.Result);
        }
    }
}
