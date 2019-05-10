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
        public void Test_AddAction()
        {
            using (var engine = new JobEngine())
            {               
                var job = engine.Start("test.add",
                     new Dictionary<string, object>() {
                        { "num1", 100L },
                        { "num2", "200" },
                     });
                Task.WaitAll(job.Task);
                var result = job.Task.Result;
                Assert.AreEqual(result.IsSuccess, true);
                Assert.AreEqual(result.Result,300);
            }
        }
        [TestMethod()]
        public void Test_LongJob()
        {
            using (var engine = new JobEngine())
            {
                var job = engine.Start("test.longjob",null);
                Task.Delay(500).ContinueWith((a) =>
                {
                    engine.Cancel(job.ExecutionId);
                });
                Task.WaitAll(job.Task);
                var result = job.Task.Result;
                Assert.AreEqual(result.IsSuccess, false);
                Assert.IsNotNull(result.Error);
                Assert.AreEqual(result.Error.GetType(), typeof(OperationCanceledException));
            }
        }
    }
}
