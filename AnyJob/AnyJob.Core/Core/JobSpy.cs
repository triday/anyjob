using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AnyJob
{
    public class JobSpy:CancellationTokenSource,IJobSpy
    {
    }
}
