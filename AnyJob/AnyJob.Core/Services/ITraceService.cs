using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ITraceService
    {
        void Schedule(ExecuteContext context);
        void Running(ExecuteContext context);
        void Cancel(ExecuteContext context);
        void Success(ExecuteContext context);
    }
}
