using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyJob
{
    public class Job
    {
        public string JobId { get; set; }

        public ActionEntry ActionEntry { get; set; }

        public Task<ExecuteResult> Task { get; set; }

        public CancellationTokenSource CancelTokenSource { get; set; }
    }
}
