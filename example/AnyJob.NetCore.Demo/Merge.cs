using System.Collections.Generic;
using System.Linq;
namespace AnyJob.NetCore.Demo
{
    public class Merge : IAction
    {
        public List<PersonInfo> Persons { get; set; }

        public PersonInfo Other { get; set; }

        public object Run(IActionContext context)
        {
            var result = new List<PersonInfo>();

            result.AddRange(Persons ?? Enumerable.Empty<PersonInfo>());
            if (Other != null)
            {
                result.Add(Other);
            }
            return result;
        }
    }

    public class PersonInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
