using AnyJob.Process;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AnyJob.Node
{
    public class NodeAction : TypedProcessAction
    {
        public NodeAction(NodeOption nodeOption, string entryFile)
        {
            this.Option = nodeOption;
            this.EntryFile = entryFile;
        }
        public string EntryFile { get; private set; }
        public NodeOption Option { get; private set; }

        protected override IDictionary<string, string> OnGetEnvironment(IActionContext context)
        {
            var currentEnv = base.OnGetEnvironment(context);
            string currentNodeModulesPath = System.Environment.GetEnvironmentVariable("NODE_PATH");
            string packNodeModulesPath = System.IO.Path.GetFullPath(Option.PackNodeModulesPath, context.RuntimeInfo.WorkingDirectory);
            string globalNodeModulesPath = System.IO.Path.GetFullPath(Option.GlobalNodeModulesPath);
            currentEnv.Add("NODE_PATH", JoinEnvironmentPaths(packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        private string JoinEnvironmentPaths(params string[] paths)
        {
            return string.Join(System.IO.Path.PathSeparator, paths.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.Trim(System.IO.Path.PathSeparator)));
        }
        protected override (string FileName, string Arguments) OnGetCommands(IActionContext context)
        {
            ISerializeService serializeService = context.ServiceProvider.GetService<ISerializeService>();
            string wrapperPath = System.IO.Path.GetFullPath(Option.WrapperPath, Environment.CurrentDirectory);
            string paramsText = serializeService.Serialize(context.Parameters.Inputs ?? new object());
            string entryFile = System.IO.Path.GetFullPath(this.EntryFile, context.RuntimeInfo.WorkingDirectory);
            string[] args = new string[] {
                wrapperPath,
                entryFile,
                "json",
                paramsText.Replace("\"","\\\"")
            };
            return (Option.NodePath, string.Join(' ', args));
        }
    }
}
