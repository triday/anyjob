using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AnyJob.Runner.NetCore.IntegrationTest
{
    [TestClass]
    public class NetCoreActionTest
    {
        [DataTestMethod]
        [DataRow("netcorepack.add")]
        [DataRow("netcorepack.add_static")]
        [DataRow("netcorepack.add_task")]
        [DataRow("netcorepack.add_valuetask")]
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
        [DataRow("netcorepack.concat")]
        [DataRow("netcorepack.concat_static")]
        [DataRow("netcorepack.concat_task")]
        [DataRow("netcorepack.concat_valuetask")]
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
        [DataRow("netcorepack.hello")]
        [DataRow("netcorepack.hello_static")]
        [DataRow("netcorepack.hello_task")]
        [DataRow("netcorepack.hello_valuetask")]
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
        [DataRow("netcorepack.merge")]
        [DataRow("netcorepack.merge_static")]
        [DataRow("netcorepack.merge_task")]
        [DataRow("netcorepack.merge_valuetask")]
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
            var fullResult = JsonConvert.SerializeObject(result.Result, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            Assert.AreEqual("[{\"id\":1001,\"name\":\"zhangsan\"},{\"id\":1002,\"name\":\"lisi\"}]", fullResult);
        }
    }
}
