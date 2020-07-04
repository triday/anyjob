using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Workflow.Config;
using AnyJob.Workflow.Models;
using Microsoft.Extensions.Options;

namespace AnyJob.Workflow.Actions
{
    public abstract class SubGroupAction : IAction
    {
        public GroupInfo SubGroup { get; set; }
        public object Run(IActionContext context)
        {
            IOptions<WorkflowOption> option = context.GetRequiredService<IOptions<WorkflowOption>>();
            this.SubGroup = context.Parameters.Vars[option.Value.SubEntryActionVarName] as GroupInfo;
            return RunInternal(context);
        }
        protected abstract object RunInternal(IActionContext context);


    }
}
