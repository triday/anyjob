using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;

namespace AnyJob
{
    public class ActionMeta : IActionMeta
    {

        public string ActionKind { get; set; }

        public string Description { get; set; }

        public string EntryPoint { get; set; }

        public bool Enabled { get; set; }

        public IReadOnlyList<string> Tags { get; set; }

        public IReadOnlyList<IActionInputDefination> Inputs { get; set; }

        public IActionOutputDefination Output { get; set; }
    }
}
