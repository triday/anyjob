using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IJobService
    {
        Job Start(JobStartInfo jobStartInfo);
        bool Cancel(string jobId);
    }
}
