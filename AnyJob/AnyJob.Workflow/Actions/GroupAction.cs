using AnyJob.Workflow.Models;
using AnyJob.Workflow.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Actions
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
