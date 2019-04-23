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
        public ActionParameters Parameters { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}
