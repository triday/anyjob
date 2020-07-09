using System;

namespace AnyJob.Runner.Node
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]

    [YS.Knife.DictionaryKey("node")]
    public class NodeActionFactory : IActionFactoryService
    {
        public NodeActionFactory(NodeOption option)
        {
            this.option = option;
        }
        private NodeOption option;

        public IAction CreateAction(IActionContext actionContext)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            var entryFile = actionContext.MetaInfo.EntryPoint;
            if (string.IsNullOrEmpty(option.DockerImage))
            {
                return new NodeAction(option, entryFile);
            }
            else
            {
                return new DockerNodeAction(option, entryFile);
            }
        }


    }
}
