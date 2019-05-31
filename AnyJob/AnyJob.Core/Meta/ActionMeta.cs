using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public class ActionMeta : IActionMeta
    {

        public string Kind { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public IEnumerable<IActionInputMeta> Inputs { get; set; }

        public IActionOutputMeta Output { get; set; }
    }
}
