using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob
{
    public interface IActionExecuterService
    {
        //void PreExecute(ActionEntry entry, ActionContext actionContext);
        Task<ExecuteResult> Execute(IExecuteContext executeContext);
    }
}
