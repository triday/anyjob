using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AnyJob.Internal.Demo
{
    public class Merge : IAction
    {
        public List<PersonInfo> Persons { get; set; }

        public PersonInfo Other { get; set; }

        public virtual object Run(IActionContext context)
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

    public class MergeTask : IAction
    {
        public List<PersonInfo> Persons { get; set; }

        public PersonInfo Other { get; set; }

        public virtual object Run(IActionContext context)
        {
            var result = new List<PersonInfo>();

            result.AddRange(Persons ?? Enumerable.Empty<PersonInfo>());
            if (Other != null)
            {
                result.Add(Other);
            }
            return Task.FromResult(result);
        }
    }
}
