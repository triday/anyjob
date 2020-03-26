using AnyJob.DependencyInjection;
using System;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultTimeService : ITimeService
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
