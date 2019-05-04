using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AnyJob.Assembly
{
    public class ActionMeta : IActionMeta
    {
        public string Ref { get; set; }

        public string Description { get; set; }

        public string DisplayFormat { get; set; }

        public string Kind { get; set; }

        public bool Enabled { get; set; }

        public Type ActionType { get; set; }

        public IEnumerable<ActionInputMeta> InputMetas { get; set; }

        public ActionOutputMeta Output { get; set; }

        IEnumerable<IActionInputMeta> IActionMeta.InputMetas => this.InputMetas;

        IActionOutputMeta IActionMeta.Output => this.Output;
    }
}
