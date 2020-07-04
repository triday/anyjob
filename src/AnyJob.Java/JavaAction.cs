using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Process;

namespace AnyJob.Java
{
    public class JavaAction : TypedProcessAction2
    {
        public JavaAction(JavaOption option)
        {

        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            throw new NotImplementedException();
        }

    }
}
