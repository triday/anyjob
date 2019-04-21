using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ITaskEngine: IServiceContainer
    {
        TaskResult Start(string actionRef, ActionParameters actionParameters);

        bool Stop(string executionId);
        
    }
}
