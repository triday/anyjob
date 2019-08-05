using AnyJob.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        protected class TraceInfo
        {
            public string ExecuteName { get; set; }
            public IExecutePath ExecutePath { get; set; }
            public IActionName ActionName { get; set; }
            public ExecuteState State { get; set; }
            public ExecuteResult Result { get; set; }
            public IActionMeta MetaInfo { get; set; }
            public IActionRuntime RuntimeInfo { get; set; }
        }
    }
}
