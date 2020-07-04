using System.Collections.Generic;

namespace AnyJob
{
    public interface IJobStartInfo
    {
        string ActionFullName { get; }
        Dictionary<string, object> Context { get; }
        string ExecutionId { get; }
        Dictionary<string, object> Inputs { get; }
        int RetryCount { get; }
    }
}
