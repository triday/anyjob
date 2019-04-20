using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionContext:IValueProvider,IServiceProvider
    {
        IActionParameters Parameters { get; }
        ActionMeta Meta { get; }
    }
}
