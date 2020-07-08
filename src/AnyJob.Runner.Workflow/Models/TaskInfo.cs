using System.Collections.Generic;

namespace AnyJob.Runner.Workflow.Models
{
    public class TaskInfo : GroupInfo
    {
        public IDictionary<string, object> Inputs { get; set; }
        public string Action { get; set; }
        public int RetryCount { get; set; }
        public TaskChainGroup OnSuccess { get; set; }
        public TaskChainGroup OnError { get; set; }
        public TaskChainGroup OnComplete { get; set; }
    }
}
