using System.Runtime.InteropServices;

namespace AnyJob
{
    public class ActionRuntime : IActionRuntime
    {
        public string WorkingDirectory { get; set; }
        public OSPlatform OSPlatForm { get; set; }
        //public string NetCoreVersion { get; set; }
    }
}
