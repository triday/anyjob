using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace AnyJob
{
    [TestClass]
    public class LocalTest
    {
        [TestMethod]
        public void TestPathCombin()
        {
            string path = System.IO.Path.Combine("abc", "", "");
        }
        [TestMethod]
        public void TestJsonSchema()
        {

            var schema = JSchema.Parse(@"{
  'type': 'number'
}");
            var obj = JToken.FromObject(1);

            obj.Validate(schema);

        }
    }
}
