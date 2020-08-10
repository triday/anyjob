using System;
using System.Collections.Generic;
using System.IO;
using AnyJob;
using AnyJob.Runner.Process;
namespace AnyJob.Runner.NetCore
{
    public class DockerNetCoreAction : TypedProcessAction2, IAction
    {
        private readonly NetCoreEntryInfo entryInfo;
        private readonly NetCoreOptions netCoreOptions;

        public DockerNetCoreAction(NetCoreEntryInfo entryInfo, NetCoreOptions netCoreOptions)
        {
            this.entryInfo = entryInfo;
            this.netCoreOptions = netCoreOptions;
        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            string RootDirInDocker = "/anyjob";
            string PackageDirInDocker = System.IO.Path.Combine(RootDirInDocker, "packs", context.Name.Pack).ToUnixPath();

            string wrapperDirInLocal = Path.GetFullPath(Path.GetDirectoryName(netCoreOptions.WrapperPath));
            string wrapperDirInDocker = Path.Combine(RootDirInDocker, "global/netcore/").ToUnixPath();
            string wrapperPathInDocker = Path.Combine(wrapperDirInDocker, Path.GetFileName(netCoreOptions.WrapperPath)).ToUnixPath();


            string exchangePathInDocker = Path.Combine(RootDirInDocker, "exchange").ToUnixPath();
            string inputFileInDocker = Path.Combine(exchangePathInDocker, Path.GetFileName(inputFile)).ToUnixPath();
            string outputFileInDocker = Path.Combine(exchangePathInDocker, Path.GetFileName(outputFile)).ToUnixPath();
            return ProcessExecuter.BuildDockerProcess(netCoreOptions.DockerImage,
                new[] { netCoreOptions.DotnetPath, wrapperPathInDocker, entryInfo.Assembly, entryInfo.Type, entryInfo.Method, inputFileInDocker, outputFileInDocker },
                PackageDirInDocker,
                new Dictionary<string, string>
                {
                    [context.RuntimeInfo.WorkingDirectory] = PackageDirInDocker,
                    [wrapperDirInLocal] = wrapperDirInDocker,
                    [exchangePath] = exchangePathInDocker
                },
                new Dictionary<string, string>
                {
                },
                string.Empty
                );
        }
    }
}
