using System;
using System.Collections.Generic;
using System.Net.Http;
using AnyJob.Config;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultPackageProviderService : IPackageProviderService
    {
        readonly PackOption packOption;
        readonly ISerializeService serializeService;
        public DefaultPackageProviderService(PackOption packOption, ISerializeService serializeService)
        {
            this.packOption = packOption;
            this.serializeService = serializeService;
        }
        public List<PackageFileInfo> GetAllPackageFiles(string provider, string package, string version)
        {
            var baseUrl = GetServiceBaseUrl(provider);
            var baseUri = new Uri(baseUrl);
            using (var client = new HttpClient())
            {
                var fullUrl = new Uri(baseUri, $"packages/{package}/{version}/files");
                var content = client.GetStringAsync(fullUrl).GetAwaiter().GetResult();
                var packageFiles = serializeService.Deserialize<List<PackageFileInfo>>(content);
                packageFiles.ForEach(f =>
                {
                    f.FileUrl = new Uri(baseUri, f.FileUrl).ToString();
                });
                return packageFiles;
            }
        }

        public string GetLatestPackageVersion(string provider, string package)
        {
            var baseUrl = GetServiceBaseUrl(provider);
            var baseUri = new Uri(baseUrl);
            using (var client = new HttpClient())
            {
                var fullUrl = new Uri(baseUri, $"packages/{package}/versions/latest");
                var content = client.GetStringAsync(fullUrl).GetAwaiter().GetResult();
                return serializeService.Deserialize<PackageVersionInfo>(content).Version;
            }
        }

        private string GetServiceBaseUrl(string provider)
        {
            if (packOption.Providers != null && packOption.Providers.TryGetValue(provider, out string url))
            {
                return url;
            }
            else
            {
                throw new Exception($"Can not find package provider '{provider}'.");
            }
        }
        class PackageVersionInfo
        {
            public string Version { get; set; }
            public string Description { get; set; }
            public List<string> Tags { get; set; }
        }
    }
}
