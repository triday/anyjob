using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Models
{
    public class WorkflowInfo
    {
        public string Version { get; set; }

        public GroupInfo Setup { get; set; }

        public GroupInfo Body { get; set; }

        public GroupInfo TearDown { get; set; }

        public Dictionary<string, object> GlobalVars { get; set; }

        public Object Output { get; set; }
    }
}
