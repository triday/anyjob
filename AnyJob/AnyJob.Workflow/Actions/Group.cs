using AnyJob.Workflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Actions
{
    public class Group : IGroupAction
    {
        public GroupInfo GroupInfo { get; set; }

        public object Run(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
