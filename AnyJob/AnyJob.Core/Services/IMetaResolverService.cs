using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IMetaResolverService
    {
        IActionMeta ResolveMeta(string refId);
    }
}
