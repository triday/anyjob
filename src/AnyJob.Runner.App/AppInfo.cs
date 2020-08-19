using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AnyJob.Runner.App
{
    public class AppInfo
    {
        public string Command { get; set; }
        public Dictionary<string, string> Envs { get; set; }
    }
}
