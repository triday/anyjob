using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AnyJob
{
    public interface IActionRuntime
    {
        string WorkingDirectory { get; set; }
        OSPlatform OSPlatForm { get; set; }
        //string NetCoreVersion { get; set; }

    }

}
