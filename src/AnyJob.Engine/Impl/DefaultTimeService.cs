using System;
using AnyJob.DependencyInjection;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultTimeService : ITimeService
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
