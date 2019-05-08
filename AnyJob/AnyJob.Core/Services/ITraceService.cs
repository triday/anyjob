using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ITraceService
    {
        void TraceState(IExecuteContext context, ExecuteState state, ExecuteResult result);
    }
}
