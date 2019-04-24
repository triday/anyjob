using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Actions
{
    public class Workflow : IAction
    {
        public object Run(IActionContext context)
        {
            return true;
        }
    }
}
