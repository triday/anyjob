using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace AnyJob.Impl
{
    public static class Extentions
    {
        public static IServiceCollection ConfigAssemblyServices(this IServiceCollection services, Assembly assembly, Func<Type, Type, bool> filter = null)
        {
            filter = filter == null ? ((a, b) => false) : filter;
            var mapInfos = from p in assembly.GetTypes()
                           where p.IsClass && !p.IsAbstract && Attribute.IsDefined(p, typeof(ServiceImplClass))
                           let attr = Attribute.GetCustomAttribute(p, typeof(ServiceImplClass)) as ServiceImplClass
                           where !filter(p, attr.InjectType)
                           select new
                           {
                               InstanceType = p,
                               attr.InjectType,
                               attr.Lifetime
                           };
            foreach (var map in mapInfos)
            {
                switch (map.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(map.InjectType, map.InstanceType);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(map.InjectType, map.InstanceType);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(map.InjectType, map.InstanceType);
                        break;
                }
            }
            return services;
        }
        public static IServiceCollection ConfigAssemblyServices(this IServiceCollection services, IEnumerable<Assembly> assemblies, Func<Type, Type, bool> filter = null)
        {
            foreach (var assembly in assemblies)
            {
                ConfigAssemblyServices(services, assembly,filter);
            }
            return services;
        }
    }
}
