using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class ExecuteContext : IExecuteContext
    {
        public string ActionFullName { get; set; }

        public ActionParameters ActionParameters { get; set; }

        public int ActionRetryCount { get; set; }

        public CancellationToken Token { get; set; }

        public IExecutePath ExecutePath { get; set; }
    }
}
