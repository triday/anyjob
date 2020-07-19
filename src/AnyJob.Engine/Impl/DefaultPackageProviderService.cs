using System;
using System.Collections.Generic;
using System.Net.Http;
using AnyJob.Config;

namespace AnyJob.Impl
{
    public class DefaultPackageProviderService : IPackageProviderService
    {
        readonly PackOption packOption;
        readonly ISerializeService serializeService;
        public DefaultPackageProviderService(PackOption packOption, ISerializeService serializeService)
        {
            this.packOption = packOption;
        }
        public List<PackageFileInfo> GetAllPackageFiles(string provider, string package, string version)
        {
            var baseUrl = GetServiceBaseUrl(provider);
            using (var client = new HttpClient())
            {
                var url = new Uri(baseUrl);
                var fullUrl = new Uri(new Uri(baseUrl), $"packages/{package}/{version}/files");
                var content = client.GetStringAsync(fullUrl).GetAwaiter().GetResult();
                return serializeService.Deserialize<List<PackageFileInfo>>(content);
            }
        }

        public string GetLatestPackageVersion(string provider, string package)
        {
            var baseUrl = GetServiceBaseUrl(provider);
            using (var client = new HttpClient())
            {
                var url = new Uri(baseUrl);
                var fullUrl = new Uri(new Uri(baseUrl), $"packages/{package}/versions/latest");
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
