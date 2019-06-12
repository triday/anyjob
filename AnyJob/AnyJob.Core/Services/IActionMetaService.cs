using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionMetaService
    {
        IActionMeta GetActionMeta(IActionName actionName);
    }
}
