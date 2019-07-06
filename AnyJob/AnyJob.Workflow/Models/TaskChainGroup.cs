using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class TaskChainGroup
    {

        public List<TaskChainInfo> Chains { get; set; }

        public Dictionary<string, object> Outputs { get; set; }
    }
}
