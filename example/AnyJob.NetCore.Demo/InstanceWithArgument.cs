using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.NetCore.Demo
{
    public class InstanceWithArgument
    {
        public InstanceWithArgument(string value)
        {
        }
        public int Add(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
