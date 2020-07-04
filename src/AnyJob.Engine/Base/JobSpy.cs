using System.Threading;

namespace AnyJob
{
    public class JobSpy : CancellationTokenSource, IJobSpy
    {
    }
}
