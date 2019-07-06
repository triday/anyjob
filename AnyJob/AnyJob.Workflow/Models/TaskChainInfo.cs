using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class TaskChainInfo
    {
        public string Stop { get; set; }

        public string Condition { get; set; }

        public string Task { get; set; }
    }
}
