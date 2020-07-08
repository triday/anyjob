using System.Collections.Generic;

namespace AnyJob.Runner.Workflow.Models
{
    public class GroupInfo
    {
        public string[] Entry { get; set; }
        public IDictionary<string, object> Vars { get; set; }
        public IDictionary<string, TaskInfo> Tasks { get; set; }
    }
}
