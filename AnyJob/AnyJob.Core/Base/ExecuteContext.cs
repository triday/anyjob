using System.Threading;

namespace AnyJob
{
    public class ExecuteContext : IExecuteContext
    {
        public string ActionFullName { get; set; }

        public IExecuteParameter ExecuteParameter { get; set; }

        public int ActionRetryCount { get; set; }

        public CancellationToken Token { get; set; }

        public IExecutePath ExecutePath { get; set; }

        public string ExecuteName { get; set; }
    }
}
