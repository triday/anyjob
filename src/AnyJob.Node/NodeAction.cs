using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnyJob;
using AnyJob.Process;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Node
{
    public class NodeAction : TypedProcessAction2
    {
        public NodeAction(NodeOption nodeOption, string entryFile)
        {
            this.Option = nodeOption;
            this.EntryFile = entryFile;
        }
        public NodeAction(string entryFile, NodeOption option)
        {
            this.EntryFile = entryFile;
            this.Option = option;

        }
        public string EntryFile { get; private set; }
        public NodeOption Option { get; private set; }

        protected virtual IDictionary<string, string> OnGetEnvironment(IActionContext context, bool inDocker)
        {
            var currentEnv = new Dictionary<string, string>();
            string currentNodeModulesPath = System.Environment.GetEnvironmentVariable("NODE_PATH");
            string packNodeModulesPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory, Option.PackNodeModulesPath));
            string globalNodeModulesPath = System.IO.Path.GetFullPath(Option.GlobalNodeModulesPath);
            currentEnv.Add("NODE_PATH", JoinEnvironmentPaths(inDocker, packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            string wrapperPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, Option.WrapperPath));
            string entryFile = Path.Combine(context.RuntimeInfo.WorkingDirectory, this.EntryFile);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                FileName = Option.NodePath,
                StandardInput = string.Empty,
                Arguments = new string[] { wrapperPath, entryFile, inputFile, outputFile },
                Envs = this.OnGetEnvironment(context, false),
            };
        }
    }
}
