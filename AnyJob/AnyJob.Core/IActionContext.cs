using AnyJob.Meta;
using System;

namespace AnyJob
{
    public interface IActionContext
    {
        ActionMeta Meta { get; }
        ActionParameters Parameters { get; }
    }
}