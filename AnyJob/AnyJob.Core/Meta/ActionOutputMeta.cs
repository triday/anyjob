using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public class ActionOutputMeta : IActionOutputMeta
    {
        public IActionType Type { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }
    }
}
