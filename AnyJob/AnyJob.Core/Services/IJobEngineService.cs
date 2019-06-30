
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IJobEngineService
    {
        Job Start(JobStartInfo jobStartInfo);
        bool Cancel(string executionId);
    }
}
