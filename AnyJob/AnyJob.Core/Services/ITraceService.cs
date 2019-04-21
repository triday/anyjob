using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ITraceService
    {
        void Schedule(TraceContext context);
        void Running(TraceContext context);
        void Cancel(TraceContext context);
        void Success(TraceContext context);
    }
}
