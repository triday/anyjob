using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    public class BaseActionExecuter : IActionExecuterService
    {
        public Task<ExecuteResult> Execute(ExecuteContext executeContext)
        {
            throw new NotImplementedException();
        }
    }
}
