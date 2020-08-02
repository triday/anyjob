using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Runner.Internal.IntegrationTest
{
    [TestClass]
    public class InternalAction
    {
        [TestMethod]
        public void ShouldInvokeSuccessWhenAddTwoArgument()
        {
            var inputs = new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            };
            var job = JobEngine.Start("internalpack.add", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(300.0, result.Result);
        }

        [TestMethod]
        public void ShouldSuccessWhenConcatLargeStrings()
        {
            var arg1 = string.Join("\n", Enumerable.Range(1, 1000000).Select(p => $"{p:d8}"));
            var arg2 = string.Join("\n", Enumerable.Range(100000000, 1000000).Select(p => $"{p:d8}"));
            var inputs = new Dictionary<string, object>()
            {
                { "a" ,arg1 },
                { "b" ,arg2 }
            };
            var job = JobEngine.Start("internalpack.concat", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            var resultText = result.Result as string;
            Assert.AreEqual(arg1.Length + arg2.Length, resultText.Length);
        }

        [TestMethod]
        public void ShouldSuccessWhenHelloActionCalled()
        {

            var inputs = new Dictionary<string, object>()
            {
                { "name" ,"Bob" }
            };
            var job = JobEngine.Start("internalpack.hello", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(null, result.Result);
            Assert.AreEqual("Hello,Bob", result.Logger.Trim());

        }
    }
}
