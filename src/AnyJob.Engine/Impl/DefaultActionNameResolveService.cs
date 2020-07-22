using System.Text.RegularExpressions;
using AnyJob.Config;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultActionNameResolveService : IActionNameResolveService
    {
        readonly PackOption packOption;
        private readonly IPackageProviderService packageProviderService;
        public DefaultActionNameResolveService(PackOption packOption, IPackageProviderService packageProviderService)
        {
            this.packOption = packOption;
            this.packageProviderService = packageProviderService;
        }
        private static Regex ActionFullnameRegex = new Regex(@"^((?<provider>\w+):)?(?<pack>\w+(\.\w+)*)\.(?<name>\w+)(@(?<version>\d+(\.\d+){1,3}))?$", RegexOptions.Compiled);

        public virtual IActionName ResolverName(string fullName)
        {
            var match = ActionFullnameRegex.Match(fullName ?? string.Empty);
            if (match.Success)
            {
                var pack = match.Groups["pack"].Value;
                var name = match.Groups["name"].Value;
                var provider = match.Groups["provider"].Success ? match.Groups["provider"].Value : packOption.DefaultProviderName;
                var version = match.Groups["version"].Success ? match.Groups["version"].Value : GetLatestPackVersion(provider, pack);
                return new ActionName
                {
                    Provider = provider,
                    Pack = pack,
                    Name = name,
                    Version = version
                };
            }
            else
            {
                throw Errors.InvalidActionName(fullName);
            }
        }
        public virtual string GetLatestPackVersion(string provider, string package)
        {
            // TODO cache the result
            return packageProviderService.GetLatestPackageVersion(provider, package);
        }
    }
}
