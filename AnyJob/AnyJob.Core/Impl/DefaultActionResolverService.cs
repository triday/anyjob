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
        private Dictionary<string, IActionFactory> factoryMaps = new Dictionary<string, IActionFactory>();

        public DefaultActionResolverService(IEnumerable<IActionFactory> factorys)
        {
            factoryMaps = factorys.ToDictionary(p => p.ActionType, StringComparer.CurrentCultureIgnoreCase);
        }
        public IAction ResolveAction(IActionMeta meta, IActionParameters parameters)
        {
            var factory = factoryMaps[meta.Kind];
            if (factory == null)
            {
                throw new ActionException($"Can not resolve action by action kind \"{meta.Kind}\".");
            }
            return factory.CreateAction(meta, parameters);
        }
    }
}
