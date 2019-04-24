using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace AnyJob.Loader
{
    public static class Extentions
    {
        public static IServiceCollection ConfigAssemblyServices(this IServiceCollection services,Assembly assembly)
        {

            var loadInstances = from p in assembly.GetTypes()
                            where p.IsClass && typeof(IAnyjobAssembly).IsAssignableFrom(p) && !p.IsAbstract && p.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(p) as IAnyjobAssembly;
            foreach (var loader in loadInstances)
            {
                loader.Registe(services);
            }
            return services;
        }
        public static IServiceCollection ConfigAssemblyServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                ConfigAssemblyServices(services, assembly);
            }
            return services;
        }
    }
}
