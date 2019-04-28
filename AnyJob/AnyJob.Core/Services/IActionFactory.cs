using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
namespace AnyJob
{
    public interface IActionFactory
    {
        string ActionType { get; }
        IAction CreateAction(IActionMeta meta, IActionParameters parameters);
    }
}
