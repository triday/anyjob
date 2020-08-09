using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AnyJob.Runner.Process;

namespace AnyJob.Runner.NetCore
{
    public class NetCoreAction : TypedProcessAction2, IAction
    {
        private readonly NetCoreEntryInfo entryInfo;
        private readonly NetCoreOptions netCoreOptions;

        public NetCoreAction(NetCoreEntryInfo entryInfo, NetCoreOptions netCoreOptions)
        {
            this.entryInfo = entryInfo;
            this.netCoreOptions = netCoreOptions;
        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            string wrapperPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, netCoreOptions.WrapperPath));
            string entryFile = Path.Combine(context.RuntimeInfo.WorkingDirectory, entryInfo.Assembly);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                FileName = netCoreOptions.DotnetPath,
                StandardInput = string.Empty,
                Arguments = new string[] { wrapperPath, entryFile, entryInfo.Type, entryInfo.Method, inputFile, outputFile },
                Envs = new Dictionary<string, string>()
            };
        }
    }
}
