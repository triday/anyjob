using System;
using System.Threading;

namespace AnyJob
{
    public interface IJobSpy
    {
        void Cancel();
        void CancelAfter(TimeSpan delay);
        CancellationToken Token { get; }
    }
}
