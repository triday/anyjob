using AnyJob.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultTraceService : ITraceService
    {
        private ILogger logger;
        public DefaultTraceService(ILogger<DefaultTraceService> logger)
        {
            this.logger = logger;
        }
        public void TraceState(IExecuteContext context, ExecuteState state, ExecuteResult result)
        {
            
            logger.LogInformation("{0}...{1} [{2}]",
                context.ExecutePath.RootId,
                context.ExecutePath.ExecuteId,
                context.ActionFullName,
                state);
        }
    }
}
