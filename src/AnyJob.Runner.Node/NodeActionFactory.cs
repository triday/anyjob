using System;
using System.Linq;
namespace AnyJob.Runner.Node
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]

    [YS.Knife.DictionaryKey("node")]
    public class NodeActionFactory : IActionFactoryService
    {
        public NodeActionFactory(NodeOptions option)
        {
            this.option = option;
        }
        private NodeOptions option;

        public IAction CreateAction(IActionContext actionContext)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            var entryInfo = ParseEntryInfo(actionContext.MetaInfo.EntryPoint);
            if (string.IsNullOrEmpty(option.DockerImage))
            {
                return new NodeAction(option, entryInfo);
            }
            else
            {
                return new DockerNodeAction(option, entryInfo);
            }
        }
        private NodeEntryInfo ParseEntryInfo(string entry)
        {
            var items = entry.Split(',').Select(p => p.Trim()).ToArray();
            if (items.Length == 2)
            {
                return new NodeEntryInfo
                {
                    Module = items[0],
                    Method = items[1]
                };
            }
            else
            {
                throw new Exception("Invalid entry format.");
            }
        }

    }
}
