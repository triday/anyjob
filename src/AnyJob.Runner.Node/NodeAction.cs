using System;
using System.Collections.Generic;
using System.IO;
using AnyJob.Runner.Process;
namespace AnyJob.Runner.Node
{
    public class NodeAction : TypedProcessAction2
    {
        public NodeAction(NodeOption nodeOption, string entryFile)
        {
            this.Option = nodeOption;
            this.EntryFile = entryFile;
        }

        public string EntryFile { get; private set; }
        public NodeOption Option { get; private set; }

        protected virtual IDictionary<string, string> OnGetEnvironment(IActionContext context, bool inDocker)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            var currentEnv = new Dictionary<string, string>();
            string currentNodeModulesPath = System.Environment.GetEnvironmentVariable("NODE_PATH");
            string packNodeModulesPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory, Option.PackNodeModulesPath));
            string globalNodeModulesPath = System.IO.Path.GetFullPath(Option.GlobalNodeModulesPath);
            currentEnv.Add("NODE_PATH", JoinEnvironmentPaths(inDocker, packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
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
