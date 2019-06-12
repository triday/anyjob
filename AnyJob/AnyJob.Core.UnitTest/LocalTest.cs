using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
