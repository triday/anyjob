using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionRuntimeService
    {
        IActionRuntime GetRunTime(IActionName actionName);
    }
}
