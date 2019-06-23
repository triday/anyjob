using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AnyJob.DependencyInjection
{
    public static class Extentions
    {
        public static IServiceCollection ConfigAssemblyServices(this IServiceCollection services, Assembly assembly, Func<Type, Type, bool> filter = null)
        {
            filter = filter == null ? ((a, b) => false) : filter;
            var mapInfos = from p in assembly.GetTypes()
                           where p.IsClass && !p.IsAbstract && Attribute.IsDefined(p, typeof(ServiceImplClassAttribute))
                           let attr = Attribute.GetCustomAttribute(p, typeof(ServiceImplClassAttribute)) as ServiceImplClassAttribute
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

        public static IServiceCollection ConfigAssemblyOptions(this IServiceCollection services, Assembly assembly, IConfiguration configuration)
        {
            var configTypes = from p in assembly.GetTypes()
                              where Attribute.IsDefined(p, typeof(ConfigClassAttribute))
                                    && !p.IsAbstract
                              select p;
            foreach (var configType in configTypes)
            {
                var configAttr = Attribute.GetCustomAttribute(configType, typeof(ConfigClassAttribute)) as ConfigClassAttribute;
                var section = configuration.GetSection(configAttr.ConfigKey);
                AddOptionInternal(services, configType, section);
            }
            return services;
        }

        private static void AddOptionInternal(IServiceCollection services, Type optionType, IConfiguration configuration)
        {
            var instance = Activator.CreateInstance(typeof(ConfigOptionProxy<>).MakeGenericType(optionType)) as IConfigOptionProxy;
            instance.Configure(services, configuration);

        }
        private interface IConfigOptionProxy
        {
            void Configure(IServiceCollection services, IConfiguration configuration);
        }
        private class ConfigOptionProxy<T> : IConfigOptionProxy
            where T : class
        {
            public void Configure(IServiceCollection services, IConfiguration configuration)
            {
                services.Configure<T>(configuration);
            }
        }
    }
}
