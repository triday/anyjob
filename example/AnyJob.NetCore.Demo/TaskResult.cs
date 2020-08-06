using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.NetCore.Demo
{
    public class TaskResult
    {
        public ValueTask<int> Add(int num1, int num2)
        {
            return new ValueTask<int>(num1 + num2);
        }

        public ValueTask<string> Concat(string a, string b)
        {
            return new ValueTask<string>(a + b);
        }
        public ValueTask Hello(string name)
        {
            Console.WriteLine($"Hello,{name}");
            return new ValueTask(Task.CompletedTask);
        }

        public ValueTask<List<PersonInfo>> Merge(List<PersonInfo> persons, PersonInfo other)
        {
            var result = new List<PersonInfo>();

            result.AddRange(persons ?? Enumerable.Empty<PersonInfo>());
            if (other != null)
            {
                result.Add(other);
            }
            return new ValueTask<List<PersonInfo>>(result);
        }
    }
}
