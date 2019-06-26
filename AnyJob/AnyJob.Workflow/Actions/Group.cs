using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow
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
