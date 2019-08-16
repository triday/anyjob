using AnyJob.Process;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Java
{
    public class JavaAction : TypedProcessAction
    {
        public JavaAction(JavaOption option)
        {

        }

        protected override (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
