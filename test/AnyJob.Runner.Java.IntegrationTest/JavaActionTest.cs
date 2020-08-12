using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AnyJob.Runner.Java.IntegrationTest
{
    [TestClass]
    public class JavaActionTest
    {
        [DataTestMethod]
        [DataRow("javapack.add")]
        //[DataRow("javapack.add_static")]
        //[DataRow("javapack.add_task")]
        //[DataRow("javapack.add_valuetask")]
        public void ShouldInvokeSuccessWhenAddTwoArgument(string addActionName)
        {
            var inputs = new Dictionary<string, object>()
            {
                { "num1" , 100 },
                { "num2" , 200 }
            };
            var job = JobEngine.Start(addActionName, inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(300L, result.Result);
        }

        [DataTestMethod]
        [DataRow("javapack.concat")]
        //[DataRow("javapack.concat_static")]
        //[DataRow("javapack.concat_task")]
        //[DataRow("javapack.concat_valuetask")]
        public void ShouldSuccessWhenConcatLargeStrings(string concatActionName)
        {
            var arg1 = string.Join("\n", Enumerable.Range(1, 1000000).Select(p => $"{p:d8}"));
            var arg2 = string.Join("\n", Enumerable.Range(100000000, 1000000).Select(p => $"{p:d8}"));
            var inputs = new Dictionary<string, object>()
            {
                { "a" ,arg1 },
                { "b" ,arg2 }
            };
            var job = JobEngine.Start(concatActionName, inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            var resultText = result.Result as string;
            Assert.AreEqual(arg1.Length + arg2.Length, resultText.Length);
        }

        [DataTestMethod]
        [DataRow("javapack.hello")]
        //[DataRow("javapack.hello_static")]
        //[DataRow("javapack.hello_task")]
        //[DataRow("javapack.hello_valuetask")]
        public void ShouldSuccessWhenHelloActionCalled(string helloActionName)
        {
            var inputs = new Dictionary<string, object>()
            {
                { "name" ,"Bob" }
            };
            var job = JobEngine.Start(helloActionName, inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(null, result.Result);
            Assert.AreEqual("Hello,Bob", result.Logger.Trim());
        }
        [DataTestMethod]
        [DataRow("javapack.merge")]
        //[DataRow("javapack.merge_static")]
        //[DataRow("javapack.merge_task")]
        //[DataRow("javapack.merge_valuetask")]
        public void ShouldSuccessWhenMergeComplexObject(string mergeActionName)
        {
            var inputs = new Dictionary<string, object>()
            {
                { "persons" , new [] {
                    new { id=1001,name="zhangsan" }
                    }
                },

                { "other", new { id=1002,name="lisi" } }
            };
            var job = JobEngine.Start(mergeActionName, inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("[{\"id\":1001,\"name\":\"zhangsan\"},{\"id\":1002,\"name\":\"lisi\"}]", ToJsonString(result.Result));
        }
        private string ToJsonString(object result)
        {
            return JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
