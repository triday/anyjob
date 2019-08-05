using AnyJob.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AnyJob.Impl
{
    [TestClass]
    public class TestDefaultSerializeService: JobTestBase
    {
        [TestMethod]
        public void TestDeserialize()
        {
            string text="{\"abc\":[\"1\",{}]}";
            var serializeService = ServiceCenter.GetRequiredService<ISerializeService>();
            var instance = serializeService.Deserialize<ModelAbc>(text);
            var arr = instance.Abc as JArray;
            var newArr = new JArray(arr.ToArray());
            Assert.IsNotNull(instance);
        }

        public class ModelAbc {
            public object Abc { get; set; }
        }
    }
}
