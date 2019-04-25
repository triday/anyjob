using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public class ActionOutputMeta
    {
        public Type Type { get; set; }

        public string TypeSchema { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }
    }
}
