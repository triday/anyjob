using System;
using System.Threading;

namespace AnyJob
{
    public interface IExecuteContext
    {
        IActionParameters ActionParameters { get; }
        string ActionRef { get; }
        CancellationTokenSource CancelTokenSource { get; }
        string ExecutionId { get; }
        string ParentExecutionId { get; }
        string RootExecutionId { get; }
    }
}