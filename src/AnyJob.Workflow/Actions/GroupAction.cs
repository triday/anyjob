using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Runner.Workflow.Models;
using AnyJob.Runner.Workflow.Services;

namespace AnyJob.Runner.Workflow.Actions
{
    public class GroupAction : SubGroupAction
    {
        protected override object RunInternal(IActionContext context)
        {
            IGroupRunnerService groupRunnerService = context.GetRequiredService<IGroupRunnerService>();
            groupRunnerService.RunGroup(context, this.SubGroup).Wait();
            return null;
        }
    }
}
