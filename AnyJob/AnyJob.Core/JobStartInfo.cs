using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class JobStartInfo
    {
        public string ActionRef { get; set; }
        public string ExecutionId { get; set; }
        public Dictionary<string, object> Inputs { get; set; }
        public Dictionary<string, object> Context { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}
