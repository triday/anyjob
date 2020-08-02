using System;
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
            _ = traceInfo ?? throw new ArgumentNullException(nameof(traceInfo));
            logger.LogInformation("{0:HH:mm:ss} {1}{2} [{3}] {4}",
                DateTime.Now,
               new string(' ', (traceInfo.ExecuteContext.ExecutePath.Depth - 1) * 4),
               traceInfo.ExecuteContext.ExecutePath.ExecuteId,
               traceInfo.ExecuteContext.ActionFullName,
               traceInfo.State);
        }
    }
}
