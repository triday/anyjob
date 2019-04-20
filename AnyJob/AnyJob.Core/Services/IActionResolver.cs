using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionResolver
    {
        ActionEntry ResolveAction(string actionRef, IActionParameters parameters);
    }
}
