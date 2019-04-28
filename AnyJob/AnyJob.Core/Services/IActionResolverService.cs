using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionResolverService
    {
        IAction ResolveAction(IActionMeta meta,IActionParameters parameters);
    }
}
