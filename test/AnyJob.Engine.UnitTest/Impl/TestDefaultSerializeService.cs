using AnyJob.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using AnyJob.DependencyInjection;

namespace AnyJob.Impl
{
    [TestClass]
    public class TestDefaultSerializeService : JobTestBase
    {
        [TestMethod]
        public void TestDeserialize()
        {
            string text = "{\"abc\":[\"1\",{}]}";
            var serializeService = this.GetRequiredService<ISerializeService>();
            var instance = serializeService.Deserialize<ModelAbc>(text);
            var arr = instance.Abc as JArray;
            var newArr = new JArray(arr.ToArray());
            Assert.IsNotNull(instance);
        }

        public void TestDeserializeWithSchema()
        {
            string text = "{}";
            var serializeService = this.GetRequiredService<ISerializeService>();
            var instance = serializeService.Deserialize<ModelAbc>(text);

        }
        public class ModelAbc
        {
            public object Abc { get; set; }
        }
        [Schema("schemas.basic-schema.json")]
        public class ModelWithSchema
        {
            public int IntProp { get; set; }
            public string StringProp { get; set; }
        }
    }
}
