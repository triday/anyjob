using System.Collections.Generic;

namespace AnyJob.Runner.Workflow.Models
{
    public class TaskChainGroup
    {

        public TaskChainInfo[] Chains { get; set; }

        public IDictionary<string, object> GlobalVars { get; set; }
    }
}
