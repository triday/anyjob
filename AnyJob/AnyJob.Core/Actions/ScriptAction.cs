using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Actions
{
    public abstract class ScriptAction : IAction
    {
        public string Workfolder { get; set; }

        public string EntryFile { get; set; }

        public abstract object Run(IActionContext context);

    }
}
