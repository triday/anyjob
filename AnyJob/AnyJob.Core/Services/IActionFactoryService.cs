using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionFactoryService
    {
        string ActionKind { get; }
        IAction CreateAction(IActionContext actionContext);
    }
}
