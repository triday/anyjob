using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnyJob.Meta;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionResolverService))]
    public class DefaultActionResolverService : IActionResolverService
    {
        IEnumerable<IActionFactory> factories;

        public DefaultActionResolverService(IEnumerable<IActionFactory> factories)
        {
            this.factories = factories.OrderBy(p => p.Priority);
        }
        public IActionEntry ResolveAction(string refName)
        {
            foreach (var factory in factories)
            {
                var entry = factory.GetEntry(refName);
                if (entry != null) return entry;
            }
            return null;
        }
    }
}
