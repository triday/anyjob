using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionFactoryService
    {
        IAction CreateAction(IActionContext actionContext);
    }
}
