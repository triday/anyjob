using System.Collections.Generic;

namespace AnyJob.NetCore.Demo
{
    public class Merge : IAction
    {
        public List<PersonInfo> Persons { get; set; }

        public PersonInfo Other { get; set; }

        public object Run(IActionContext context)
        {
            var result = new List<PersonInfo>();
            if (Persons != null)
            {
                result.AddRange(Persons);
            }
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
