using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyJob
{
    public class Job
    {
        public string ExecutionId { get; set; }

        public string ActionRef { get; set; }

        public ActionParameters ActionParameters { get; set; }

        public Task<ExecuteResult> Task { get; set; }

    }
}
