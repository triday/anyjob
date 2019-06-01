using AnyJob.Assembly.Meta;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob.UnitTest.Actions
{
    [Action()]
    public class LongJob : IAction
    {
        public object Run(IActionContext context)
        {
            for (int i = 0; i < 1000; i++)
            {
                context.Token.ThrowIfCancellationRequested();
                Thread.Sleep(100);
                Console.WriteLine("Current Time:{0:HH:mm:ss.fff}", DateTime.Now);
            }
            return null;
        }
    }
}
