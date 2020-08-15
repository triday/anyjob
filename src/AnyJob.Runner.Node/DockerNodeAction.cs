using System;
using System.Collections.Generic;
using System.IO;
using AnyJob.Runner.Process;

namespace AnyJob.Runner.Node
{
    public class DockerNodeAction : TypedProcessAction2
    {
        public DockerNodeAction(NodeOptions nodeOption, NodeEntryInfo entryInfo)
        {
            this.nodeOptions = nodeOption;
            this.entryInfo = entryInfo;
        }

        private readonly NodeEntryInfo entryInfo;
        private readonly NodeOptions nodeOptions;

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            return CreateDockerInputInfo(context, exchangePath, inputFile, outputFile);
        }

        private ProcessExecInput CreateDockerInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            string RootDirInDocker = "/anyjob";
            string PackageDirInDocker = System.IO.Path.Combine(RootDirInDocker, "packs", context.Name.Pack).ToUnixPath();
            string wrapperPathInDocker = System.IO.Path.Combine(RootDirInDocker, "node_wrapper.js").ToUnixPath();
            string globalLibDirInLocal = System.IO.Path.GetFullPath(nodeOptions.GlobalNodeModulesPath);
            string globalLibDirInDocker = System.IO.Path.Combine(RootDirInDocker, nodeOptions.GlobalNodeModulesPath).ToUnixPath();
            string exchangePathInDocker = System.IO.Path.Combine(RootDirInDocker, "exchange").ToUnixPath();
            string inputFileInDocker = System.IO.Path.Combine(exchangePathInDocker, Path.GetFileName(inputFile)).ToUnixPath();
            string outputFileInDocker = System.IO.Path.Combine(exchangePathInDocker, Path.GetFileName(outputFile)).ToUnixPath();

            string wrapperPathInLocal = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, nodeOptions.WrapperPath));
            string entryModule = Path.Combine(PackageDirInDocker, this.entryInfo.Module).ToUnixPath();
            string packNodeModulesPathInDocker = System.IO.Path.Combine(PackageDirInDocker, nodeOptions.PackNodeModulesPath).ToUnixPath();

            return ProcessExecuter.BuildDockerProcess(
                nodeOptions.DockerImage,
                new string[] { nodeOptions.NodePath, wrapperPathInDocker, entryModule, this.entryInfo.Method, inputFileInDocker, outputFileInDocker },
                PackageDirInDocker,
                new Dictionary<string, string>
                {
                    [context.RuntimeInfo.WorkingDirectory] = PackageDirInDocker,
                    [wrapperPathInLocal] = wrapperPathInDocker,
                    [exchangePath] = exchangePathInDocker,
                    [globalLibDirInLocal] = globalLibDirInDocker
                },
                new Dictionary<string, string>
                {
                    ["NODE_PATH"] = JoinEnvironmentPaths(true, PackageDirInDocker, packNodeModulesPathInDocker, globalLibDirInDocker)
                },
                string.Empty);

        }
    }
}
