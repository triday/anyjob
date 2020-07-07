using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Runner.Workflow.Models
{
    public class TaskChainGroup
    {

        public TaskChainInfo[] Chains { get; set; }

        public IDictionary<string, object> GlobalVars { get; set; }
    }
}
