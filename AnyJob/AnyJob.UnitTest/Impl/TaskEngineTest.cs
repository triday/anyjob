using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AnyJob.UnitTest.Impl
{
    [TestClass]
    class JobEngineTest
    {
        [TestMethod]
        public  void TestMethod1()
        {
            using (var engine = new JobEngine())
            {
              //var result= await engine.Execute("core.add", null);
            }
        }
    }
}
