using AnyJob.DependencyInjection;
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
            logService.Info("{0}...{1} [{2}]",
                context.ExecutePath.RootId,
                context.ExecutePath.ExecuteId,
                context.ActionFullName,
                state);
        }
    }
}
