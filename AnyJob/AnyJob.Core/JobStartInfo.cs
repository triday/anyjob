using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class JobStartInfo
    {
        public string ActionRef { get; set; }
        public Dictionary<string, object> Inputs { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> Context { get; set; } = new Dictionary<string, object>();
        public string JobId { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }
        //public int TimeoutSeconds { get; set; }
    }
}
