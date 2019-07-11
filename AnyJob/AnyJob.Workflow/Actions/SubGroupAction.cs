using AnyJob.Workflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Actions
{
    public abstract class SubGroupAction : IAction
    {
        public GroupInfo SubGroup { get; set; }
        public object Run(IActionContext context)
        {
            return RunInternal(context);
        }
        protected abstract object RunInternal(IActionContext context);


    }
}
