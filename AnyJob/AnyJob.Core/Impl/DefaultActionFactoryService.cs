using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionFactoryService))]
    public class DefaultActionFactoryService : IActionFactoryService
    {
        public DefaultActionFactoryService(IEnumerable<IActionResolverService> services)
        {
            this.resolvers = services.OrderByDescending(p=>p.Priority).ToList();
        }
        private List<IActionResolverService> resolvers;

        public ActionEntry Get(string actionRef)
        {
            foreach (var resolver in this.resolvers)
            {
                var entry = resolver.ResolveAction(actionRef);
                if (entry != null) {
                    return entry;
                }
            }
            return null;
        }
    }
}
