using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class TaskInfo
    {
        public IDictionary<string, object> Inputs { get; set; }
        public IDictionary<string, object> Vars { get; set; }
        public string ActionName { get; set; }
        public int RetryCount { get; set; }
        public TaskChainGroup OnSuccess { get; set; }
        public TaskChainGroup OnError { get; set; }
        public TaskChainGroup OnComplete { get; set; }
    }
}
