using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Workflow
{
    [TestClass]
    public class MyTestClass
    {

        [TestMethod]
        public void MyTestMethod()
        {
            int val = 0;
            var task = Task.Run(() => { return 1 / val; }).ContinueWith((p) =>
            {
                if (p.IsCompletedSuccessfully)
                {
                    Console.WriteLine("hello");
                }
                else
                {
                    Console.WriteLine(p.Exception);
                }
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Thread.Sleep(3000);


        }
    }
}
