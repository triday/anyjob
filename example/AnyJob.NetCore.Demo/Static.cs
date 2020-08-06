using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyJob.NetCore.Demo
{
    public class Static
    {
        public static int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        public static string Concat(string a, string b)
        {
            return a + b;
        }
        public static void Hello(string name)
        {
            Console.WriteLine($"Hello,{name}");
        }

        public static List<PersonInfo> Merge(List<PersonInfo> persons, PersonInfo other)
        {
            var result = new List<PersonInfo>();

            result.AddRange(persons ?? Enumerable.Empty<PersonInfo>());
            if (other != null)
            {
                result.Add(other);
            }
            return result;
        }
    }
}
