using System.Collections.Generic;

namespace AnyJob.App.Model
{
    public class AppInfo
    {
        public string Command { get; set; }
        public Dictionary<string, string> Envs { get; set; }
    }
}
