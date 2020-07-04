using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AnyJob.Impl
{
    [TestClass]
    public class TestDefaultActionNameResolveService
    {
        [TestMethod]
        public void TestNormal()
        {
            IActionNameResolveService actionNameResolveService = new DefaultActionNameResolveService();
            var testList = new List<(string Input, string Pack, string Name, string Version)>()
            {
                ("abc.bcd","abc","bcd",""),
                ("abc.bcd@1.0","abc","bcd","1.0"),
                ("abc.bcd.cde@1.0","abc.bcd","cde","1.0")
            };
            foreach (var testItem in testList)
            {
                IActionName actionName = actionNameResolveService.ResolverName(testItem.Input);
                Assert.AreEqual(testItem.Pack, actionName.Pack);
                Assert.AreEqual(testItem.Name, actionName.Name);
                Assert.AreEqual(testItem.Version, actionName.Version);
                Assert.AreEqual(testItem.Input, actionName.ToString());
            }

        }

        [TestMethod]
        public void TestFail()
        {
            IActionNameResolveService actionNameResolveService = new DefaultActionNameResolveService();
            var exp = Assert.ThrowsException<ActionException>(() =>
            {
                IActionName actionName = actionNameResolveService.ResolverName("abc");
            });
            Assert.AreEqual(exp.ErrorCode, nameof(ErrorCodes.InvalidActionName));
        }

    }
}
