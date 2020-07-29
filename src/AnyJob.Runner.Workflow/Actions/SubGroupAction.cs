using System;
using AnyJob.Runner.Workflow.Config;
using AnyJob.Runner.Workflow.Models;
using Microsoft.Extensions.Options;

namespace AnyJob.Runner.Workflow.Actions
{
    public abstract class SubGroupAction : IAction
    {
        public GroupInfo SubGroup { get; set; }
        public object Run(IActionContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            IOptions<WorkflowOption> option = context.GetRequiredService<IOptions<WorkflowOption>>();
            this.SubGroup = context.Parameters.Vars[option.Value.SubEntryActionVarName] as GroupInfo;
            return RunInternal(context);
        }
        protected abstract object RunInternal(IActionContext context);


    }
}
