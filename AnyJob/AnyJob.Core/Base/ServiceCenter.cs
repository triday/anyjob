using AnyJob.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public static class ServiceCenter
    {
  
        public static IServiceProvider CurrentProvider { get; set; }

        public static T GetRequiredService<T>()
        {
            return CurrentProvider.GetRequiredService<T>();
        }

        public static void RegisteDomainService()
        {
            CurrentProvider = RegisteAll();
        }

        private static IServiceProvider RegisteAll()
        {
            var serviceCollection = new ServiceCollection();
            var configurationRoot = InitConfiguration();
            RegisterCurrentDomainConfigs(serviceCollection, configurationRoot);
            RegisterCurrentDomainServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private static IConfigurationRoot InitConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddCommandLine(Environment.GetCommandLineArgs());
            return builder.Build();
        }


        private static void RegisterCurrentDomainConfigs(ServiceCollection services, IConfiguration configuration)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.ConfigAssemblyOptions(assembly, configuration);
            }
        }

        private static void RegisterCurrentDomainServices(ServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.ConfigAssemblyServices(assembly, null);
            }
        }
    }
}
