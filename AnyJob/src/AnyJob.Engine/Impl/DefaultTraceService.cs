using AnyJob.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultTraceService : ITraceService
    {
        private ILogger logger;
        public DefaultTraceService(ILogger<DefaultTraceService> logger)
        {
            this.logger = logger;
        }
        public void TraceState(ITraceInfo traceInfo)
        {
            logger.LogInformation("{0}...{1} [{2}]",
               traceInfo.ExecuteContext.ExecutePath.RootId,
               traceInfo.ExecuteContext.ExecutePath.ExecuteId,
               traceInfo.ExecuteContext.ActionFullName,
               traceInfo.State);
        }
    }
}
