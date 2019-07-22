using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Services
{
    public interface IWorkflowResultParseService
    {
        object ParseResult(IActionContext actionContext, object resultDesc);
    }
}
