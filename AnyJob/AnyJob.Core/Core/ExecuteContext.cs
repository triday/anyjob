using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class ExecuteContext : IExecuteContext
    {
        public string ExecutionId { get; set; }

        public string ParentExecutionId { get; set; }

        public string RootExecutionId { get; set; }

        public string ActionRef { get; set; }

        public IActionParameters ActionParameters { get; set; }

        public IExecuteSpy ExecuteSpy { get; set; }

        public int ExecutionDepth { get; set; }

        public int ActionRetryCount { get; set; }


    }
}
