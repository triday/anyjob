using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Default
{
    public class DefaultActionExecuter : IActionExecuterService
    {
        public Task<ExecuteResult> Execute(ActionEntry entry, ActionContext actionContext)
        {
            
            throw new NotImplementedException();
        }

        public Task<ExecuteResult> Execute(ExecuteContext executeContext)
        {
            throw new NotImplementedException();
        }
    }
}
