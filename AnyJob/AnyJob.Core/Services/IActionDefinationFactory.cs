using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
namespace AnyJob
{
    public interface IActionDefinationFactory
    {
        string ActionKind { get; }
        IActionDefination GetActionDefination(IActionRuntime runtimeInfo, IActionMeta metaInfo);
    }
}
