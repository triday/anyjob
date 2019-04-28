using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
using System.Linq;

namespace AnyJob.Impl
{
    public class DefaultMetaResolverService : IMetaResolverService
    {
        IEnumerable<IMetaFactory> factories;
        public DefaultMetaResolverService(IEnumerable<IMetaFactory> factories)
        {
            this.factories = factories.OrderBy(p => p.Priority);
        }
        public IActionMeta ResolveMeta(string refId)
        {
            foreach (var factory in factories)
            {
                var meta = factory.GetMeta(refId);
                if (meta != null) return meta;
            }
            return null;
        }
    }
}
