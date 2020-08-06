using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Runner.NetCore.Wrapper
{
    public class Test
    {
        public ValueTask Add(int num1, int num2)
        {
            return new ValueTask(Task.CompletedTask);
        }
    }
}
