using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionNameResolveService
    {
        IActionName ResolverName(string fullName);
    }
}
