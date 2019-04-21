using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyJob
{
    public class TaskResult
    {
        public string ExecuteId { get; set; }

        public ActionEntry ActionEntry { get; set; }

        public Task<ExecuteResult> Task { get; set; }

        public CancellationToken CancelToken { get; set; }
    }
}
