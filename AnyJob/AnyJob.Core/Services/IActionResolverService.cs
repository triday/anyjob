using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionResolverService
    {
        int Priority { get; }
        ActionEntry ResolveAction(string actionRef);
    }
}
