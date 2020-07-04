using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyJob;
using AnyJob.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Node
{
    [TestClass]
    public class IntegrationTest : JobTestBase
    {
        [TestMethod]
        public void TestAdd()
        {
            var inputs = new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            };
            var job = JobEngine.Start("nodepack.add", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(300L, result.Result);
        }
        [TestMethod]
        public void TestLargeInputData()
        {
            var arg1 = string.Join("\n", Enumerable.Range(1, 1000000).Select(p => $"{p:d8}"));
            var arg2 = string.Join("\n", Enumerable.Range(100000000, 1000000).Select(p => $"{p:d8}"));
            var inputs = new Dictionary<string, object>()
            {
                { "a" ,arg1 },
                { "b" ,arg2 }
            };
            var job = JobEngine.Start("nodepack.concat", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            var resultText = result.Result as string;
            Assert.AreEqual(arg1.Length + arg2.Length, resultText.Length);
        }
    }
}
