using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class GroupInfo
    {
        public string[] Entry { get; set; }
        public IDictionary<string, TaskInfo> Tasks { get; set; }
    }
}
