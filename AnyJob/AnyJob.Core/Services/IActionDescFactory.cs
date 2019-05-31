using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
namespace AnyJob
{
    public interface IActionDescFactory
    {
        string ActionKind { get; }
        IActionDesc GetActionDesc(IActionEntry entryInfo);
    }
}
