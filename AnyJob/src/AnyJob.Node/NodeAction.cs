using AnyJob.Process;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
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
            string packNodeModulesPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory, Option.PackNodeModulesPath));
            string globalNodeModulesPath = System.IO.Path.GetFullPath(Option.GlobalNodeModulesPath);
            currentEnv.Add("NODE_PATH", JoinEnvironmentPaths(packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        private string JoinEnvironmentPaths(params string[] paths)
        {
            return string.Join(Path.PathSeparator.ToString(), paths.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.Trim(System.IO.Path.PathSeparator)));
        }
        protected override (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context)
        {
            ISerializeService serializeService = context.ServiceProvider.GetService<ISerializeService>();
            string wrapperPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, Option.WrapperPath));
            string paramsText = serializeService.Serialize(context.Parameters.Arguments ?? new object());
            string entryFile = Path.Combine(context.RuntimeInfo.WorkingDirectory, this.EntryFile);
            string[] args = new string[] { wrapperPath, entryFile };
            return (Option.NodePath, string.Join(" ", args), paramsText);
        }
    }
}
