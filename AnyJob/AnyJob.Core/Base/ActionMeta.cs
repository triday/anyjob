using System;
using System.Collections.Generic;
using System.Text;


namespace AnyJob
{
    public class ActionMeta : IActionMeta
    {

        public string Kind { get; set; }

        public string Description { get; set; }

        public string EntryPoint { get; set; }

        public bool Enabled { get; set; }

        public IReadOnlyList<string> Tags { get; set; }

        public IReadOnlyDictionary<string, IActionInputDefination> Inputs { get; set; }

        public IActionOutputDefination Output { get; set; }

    }
}
