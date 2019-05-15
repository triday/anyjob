using System;
using System.Collections.Generic;
using System.Text;
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
