using System;
using System.Collections.Generic;
using System.Text;
using AnyJob;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Assembly
{
    [TestClass]
    public class IntegrationTest : YS.Knife.Hosting.KnifeHost
    {
        [TestMethod]
        public void TestAdd()
        {
            var inputs = new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            };
            var job = JobEngine.Start("pack1.add", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(300.0, result.Result);
        }
    }
}
