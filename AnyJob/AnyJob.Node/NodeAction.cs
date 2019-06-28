using AnyJob.Process;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AnyJob.Node
{
    public class NodeAction : TypedProcessAction
    {
        public NodeAction(NodeOption nodeOption, string moduleName)
        {
            this.Option = nodeOption;
        }
        public string ModuleName { get; private set; }
        public NodeOption Option { get; private set; }
        protected override (string FileName, string Arguments) OnGetCommands(IActionContext context)
        {
            ISerializeService serializeService = context.ServiceProvider.GetService<ISerializeService>();
            string wrapperPath = System.IO.Path.GetFullPath(context.RuntimeInfo.WorkingDirectory, Option.WrapperPath);
            string paramsText = serializeService.Serialize(context.Parameters.Inputs ?? new object());

            string[] args = new string[] {
                wrapperPath,
                this.ModuleName,
                "json",
                paramsText.Replace("\"","\"\"")
            };
            return (Option.NodePath, string.Join(' ', args));
        }
    }
}
