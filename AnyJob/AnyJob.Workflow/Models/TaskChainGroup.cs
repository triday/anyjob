using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class TaskChainGroup
    {

        public TaskChainInfo[] Chains { get; set; }

        public IDictionary<string, object> Outputs { get; set; }
    }
}
