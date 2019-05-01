using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
namespace AnyJob
{
    public interface IActionFactory
    {
        int Priority { get; }
        IActionEntry GetEntry(string refName);
    }
}
