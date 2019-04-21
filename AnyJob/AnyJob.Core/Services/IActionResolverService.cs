using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionResolverService
    {
        ActionEntry ResolveAction(string actionRef, ActionParameters parameters);
    }
}
