using System.Collections.Generic;


namespace AnyJob
{
    public class ActionMeta : IActionMeta
    {

        public string Kind { get; set; }

        public string Description { get; set; }

        public string EntryPoint { get; set; }

        public bool Enabled { get; set; }

        public IReadOnlyList<string> Tags { get; set; }

        public IDictionary<string, IActionType> Inputs { get; set; }

        public IActionType Output { get; set; }

    }
}
