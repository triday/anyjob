using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionResolverService))]
    public class PackActionResolver : IActionResolverService
    {
        public int Priority => 1000;

        public ActionEntry ResolveAction(string actionRef)
        {
            return null;
        }
    }
}
