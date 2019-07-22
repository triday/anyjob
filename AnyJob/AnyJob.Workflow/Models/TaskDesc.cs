using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class TaskDesc
    {
        public string TaskName { get; set; }
        public string FromName { get; set; }
        public TaskInfo TaskInfo { get; set; }
    }
}
