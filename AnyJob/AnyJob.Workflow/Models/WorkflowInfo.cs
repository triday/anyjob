using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class WorkflowInfo
    {
        public GroupInfo Setup { get; set; }

        public GroupInfo Body { get; set; }

        public GroupInfo TearDown { get; set; }
    }
}
