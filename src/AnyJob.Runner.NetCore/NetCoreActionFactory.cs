using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife;

namespace AnyJob.Runner.NetCore
{
    [ServiceClass(Lifetime = ServiceLifetime.Singleton)]
    [DictionaryKey("netcore")]
    public class NetCoreActionFactory : IActionFactoryService
    {
        private readonly NetCoreError netCoreError;
        private readonly NetCoreOptions netCoreOptions;
        public NetCoreActionFactory(NetCoreError netCoreError, NetCoreOptions netCoreOptions)
        {
            this.netCoreError = netCoreError;
            this.netCoreOptions = netCoreOptions;
        }
        public IAction CreateAction(IActionContext actionContext)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            var entryInfo = ParseEntryInfo(actionContext.MetaInfo.EntryPoint);
            if (string.IsNullOrEmpty(netCoreOptions.DockerImage))
            {
                return new NetCoreAction(entryInfo, netCoreOptions);
            }
            else
            {
                return new DockerNetCoreAction(entryInfo, netCoreOptions);
            }

        }

        private NetCoreEntryInfo ParseEntryInfo(string entry)
        {
            var items = entry.Split(',').Select(p => p.Trim()).ToArray();
            if (items.Length != 3)
            {
                throw netCoreError.InvalidEntryFormat(entry);
            }
            return new NetCoreEntryInfo
            {
                Assembly = items[0],
                Type = items[1],
                Method = items[2],
            };
        }




    }
}
