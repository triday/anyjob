using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob
{
    public interface IActionExecuter
    {
        Task<ActionResult> Execute(ActionEntry entry, IActionContext actionContext);
    }
}
