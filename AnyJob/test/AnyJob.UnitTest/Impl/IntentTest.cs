using AnyJob.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.UnitTest.Impl
{
    [TestClass]
    public class IntentTest
    {
        [TestMethod]
        public void Test_Add100Intent()
        {

            using (var engine = new JobEngine())
            {
                var job = engine.Start("test.add100",
                     new Dictionary<string, object>() {
                        { "other", 345L },
                     });
                Task.WaitAll(job.Task);
                var result = job.Task.Result;
                Assert.AreEqual(result.IsSuccess, true);
                Assert.AreEqual(result.Result, 445);
            }
        }
    }
}
