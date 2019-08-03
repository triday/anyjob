using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.DependencyInjection
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ServiceImplClassAttribute : Attribute
    {
        //public ServiceImplClassAttribute(Type injectType=null, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        //{
        //    this.InjectType = injectType;
        //    this.Lifetime = serviceLifetime;
        //}
        
        public string Key { get; set; }
        public Type InjectType { get; private set; }
        public ServiceLifetime Lifetime { get; set; }
    }
}
