using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;

namespace AnyJob
{
    [TestClass]
    public class TestJsonSchemaType
    {
        [TestMethod]
        public void TestIsMust()
        {
            var schemaType = FromFile("simple.json");
            Assert.IsNotNull(schemaType);
            Assert.AreEqual(true, schemaType.IsMust);
        }
        [TestMethod]
        public void TestDefaultValue()
        {
            var schemaType = FromFile("simple.json");
            Assert.AreEqual(100L, schemaType.DefaultValue);
        }
        [TestMethod]
        public void TestMaskProperty()
        {
            var schemaType = FromFile("simple.json");
            var obj = schemaType.MaskSecret("123456");
        }

        public JsonSchemaType FromFile(string fileName)
        {
            string fullFile = System.IO.Path.Combine("schemas", fileName);
            string content = System.IO.File.ReadAllText(fullFile);
            var schema = JSchema.Parse(content);
            return new JsonSchemaType(schema);
        }
    }
}
