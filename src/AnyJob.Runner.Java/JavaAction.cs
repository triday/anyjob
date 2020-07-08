using System;
using AnyJob.Runner.Process;

namespace AnyJob.Runner.Java
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
