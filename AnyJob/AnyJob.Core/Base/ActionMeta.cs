using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionMeta : IActionMeta
    {
        public IActionName Name { get; set; }

        public string ActionKind { get; set; }

        public string Description { get; set; }

        public string EntryPoint { get; set; }

        public bool Enabled { get; set; }

    }
}
