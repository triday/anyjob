using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AnyJob.Node
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
