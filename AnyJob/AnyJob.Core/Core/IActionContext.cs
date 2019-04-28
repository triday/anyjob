using AnyJob.Meta;
using System;

namespace AnyJob
{
    public interface IActionContext
    {
        IActionMeta Meta { get; }
        IActionParameters Parameters { get; }
    }
}