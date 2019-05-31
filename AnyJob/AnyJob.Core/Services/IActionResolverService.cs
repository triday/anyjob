using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionResolverService
    {
        IActionDesc ResolveActionDesc(IActionName actionName);
    }
}
