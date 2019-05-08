using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(ITraceService))]
    public class DefaultTraceService : ITraceService
    {
        private ILogService logService;
        public DefaultTraceService(ILogService logService)
        {
            this.logService = logService;
        }
        public void TraceState(IExecuteContext context, ExecuteState state, ExecuteResult result)
        {
            logService.Info("RootId:{0},ParentId:{1},ExectionId:{2},ActionRef:\"{3}\",State:[{4}]",
                context.RootExecutionId,
                context.ParentExecutionId,
                context.ExecutionId,
                context.ActionRef,
                state);
        }
    }
}
