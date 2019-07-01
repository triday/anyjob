using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Node
{
    [ServiceImplClass(typeof(IActionFactoryService))]
    public class NodeActionFactory : IActionFactoryService
    {
        public NodeActionFactory(IOptions<NodeOption> option)
        {
            this.option = option;
        }
        private IOptions<NodeOption> option;
        public string ActionKind => "node";

        public IAction CreateAction(IActionContext actionContext)
        {
            var entryFile = actionContext.MetaInfo.EntryPoint;
            return new NodeAction(option.Value, entryFile);
        }
    }
}
