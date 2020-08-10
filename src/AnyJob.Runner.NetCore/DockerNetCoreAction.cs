using AnyJob.Runner.Process;
namespace src.AnyJob.Runner.NetCore
{
    public class DockerNetCoreAction: TypedProcessAction2, IAction
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
            
        }
    }
}