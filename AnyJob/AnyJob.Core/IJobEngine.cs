
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IJobEngine:IDisposable
    {
        Job Start(JobStartInfo jobStartInfo);
        bool Cancel(string executionId);
    }
}
