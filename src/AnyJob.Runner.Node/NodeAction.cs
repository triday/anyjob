using System;
using System.Collections.Generic;
using System.IO;
using AnyJob.Runner.Process;
namespace AnyJob.Runner.Node
{
    public class NodeAction : TypedProcessAction2
    {
        public NodeAction(NodeOptions nodeOption, NodeEntryInfo entryInfo)
        {
            this.nodeOptions = nodeOption;
            this.entryInfo = entryInfo;
        }

        private readonly NodeEntryInfo entryInfo;
        private readonly NodeOptions nodeOptions;

        protected virtual IDictionary<string, string> OnGetEnvironment(IActionContext context, bool inDocker)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            var currentEnv = new Dictionary<string, string>();
            string currentNodeModulesPath = System.Environment.GetEnvironmentVariable("NODE_PATH");
            string packNodeModulesPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory, nodeOptions.PackNodeModulesPath));
            string globalNodeModulesPath = System.IO.Path.GetFullPath(nodeOptions.GlobalNodeModulesPath);
            currentEnv.Add("NODE_PATH", JoinEnvironmentPaths(inDocker, packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            string wrapperPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, nodeOptions.WrapperPath));
            string entryFile = Path.Combine(context.RuntimeInfo.WorkingDirectory, this.entryInfo.Module);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                FileName = nodeOptions.NodePath,
                StandardInput = string.Empty,
                Arguments = new string[] { wrapperPath, entryFile, this.entryInfo.Method, inputFile, outputFile },
                Envs = this.OnGetEnvironment(context, false),
            };
        }
    }
}
