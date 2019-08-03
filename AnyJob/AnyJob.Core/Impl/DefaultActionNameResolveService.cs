using AnyJob.DependencyInjection;
using System.Text.RegularExpressions;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultActionNameResolveService : IActionNameResolveService
    {
        public static Regex ActionFullnameRegex = new Regex(@"^(?<pack>\w+(\.\w+)*)\.(?<name>\w+)(@(?<version>\d+(\.\d+){1,3}))?$", RegexOptions.Compiled);

        public virtual IActionName ResolverName(string fullName)
        {
            var match = ActionFullnameRegex.Match(fullName);
            if (match.Success)
            {
                return new ActionName
                {
                    Pack = match.Groups["pack"].Value,
                    Name = match.Groups["name"].Value,
                    Version = match.Groups["version"].Value
                };
            }
            else
            {
                throw ActionException.FromErrorCode(nameof(ErrorCodes.InvalidActionName), fullName);
            }
        }
    }
}
