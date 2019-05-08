using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob
{
    public interface IActionExecuterService
    {
        
        Task<ExecuteResult> Execute(IExecuteContext executeContext);

        //void Cancel(string executeId);
    }


}
