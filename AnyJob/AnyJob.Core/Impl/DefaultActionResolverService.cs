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
        Dictionary<string,IActionDescFactory> factories;
        IActionEntryService actionEntryService;
        public DefaultActionResolverService(IActionEntryService finderService,IEnumerable<IActionDescFactory> factories)
        {
            this.actionEntryService = finderService;
            this.factories = factories.ToDictionary(p => p.ActionKind, StringComparer.CurrentCultureIgnoreCase);
        }
        public IActionDesc ResolveActionDesc(IActionName actionName)
        {
            var pack = actionName.Pack;

            var entryInfo = actionEntryService.GetActionEntry(actionName);

            var factory = this.factories[entryInfo.ActionKind];

            return factory.GetActionDesc(entryInfo);
        }
    }
}
