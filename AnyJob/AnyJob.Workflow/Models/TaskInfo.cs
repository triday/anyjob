using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class TaskInfo
    {
        public Dictionary<string, object> Inputs { get; set; }

        public TaskChainGroup OnSuccess { get; set; }
        public TaskChainGroup OnError { get; set; }
        public TaskChainGroup OnComplete { get; set; }
    }
}
