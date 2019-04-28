using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ITraceService
    {
        void Schedule(IExecuteContext context);
        void Running(IExecuteContext context);
        void Cancel(IExecuteContext context);
        void Success(IExecuteContext context);
    }
}
