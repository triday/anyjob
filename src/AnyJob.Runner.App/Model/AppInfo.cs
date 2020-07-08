using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AnyJob.Runner.App.Model
{
    public class AppInfo
    {
        public string[] SupportPlatforms { get; set; } = new string[] { nameof(OSPlatform.Windows), nameof(OSPlatform.Linux), nameof(OSPlatform.OSX) };
        public string Command { get; set; }
        public Dictionary<string, string> Envs { get; set; }
    }
}
