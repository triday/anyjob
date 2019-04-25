using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ServiceImplClass : Attribute
    {
        public ServiceImplClass(Type injectType, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            this.InjectType = injectType;
            this.Lifetime = serviceLifetime;
        }
        public Type InjectType { get; private set; }
        public ServiceLifetime Lifetime { get; set; }
    }
}
