using System;
using System.Collections.Generic;
using System.Text;

namespace env
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("PATH"));
        }
    }
}
