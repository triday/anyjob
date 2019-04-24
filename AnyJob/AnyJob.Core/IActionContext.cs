using AnyJob.Meta;
using System;

namespace AnyJob
{
    public interface IActionContext:IServiceProvider
    {
        ActionMeta Meta { get; }
        ActionParameters Parameters { get; }
    }
}