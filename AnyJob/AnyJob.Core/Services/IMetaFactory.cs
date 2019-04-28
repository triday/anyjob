using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;

namespace AnyJob
{
    public interface IMetaFactory
    {
        int Priority { get; }
        IActionMeta GetMeta(string refId);
    }
}
