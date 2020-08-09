using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Runner.NetCore
{
    public class NetCoreEntryInfo
    {
        public string Assembly { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
    }
}
