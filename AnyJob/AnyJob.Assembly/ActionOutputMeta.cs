using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Assembly
{
    public class ActionOutputMeta : IActionOutputMeta
    {
        public string Type { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public Type OutputType { get; set; }
    }
}
