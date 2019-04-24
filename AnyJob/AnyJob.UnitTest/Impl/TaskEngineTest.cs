using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnyJob.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AnyJob.UnitTest.Impl
{
    [TestClass]
    public class JobEngineTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var engine = new JobEngine())
            {
                engine.ConfigServices();
                var job = engine.Start("test.add",
                     new Dictionary<string, object>() {
                        { "num1", 1 },
                        { "num2", 2 },
                     });
                Task.WaitAll(job.Task);
                var result = job.Task.Result;
                Assert.AreEqual(result.IsSuccess, true);
                Assert.AreEqual(result.Result, 3);
            }
        }
    }
}
