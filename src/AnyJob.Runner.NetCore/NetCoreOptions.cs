using System;
using System.Collections.Generic;
using System.Text;
using YS.Knife;

namespace AnyJob.Runner.NetCore
{

    [OptionsClass("netcore")]
    public class NetCoreOptions
    {
        public string DockerImage { get; set; } = "mcr.microsoft.com/dotnet/core/runtime:3.1";
        public string DotnetPath { get; set; } = "dotnet";
        public string WrapperPath { get; set; } = "global/netcore/netcoreapp3.1/NetCore_Wrapper.dll";
    }
}
