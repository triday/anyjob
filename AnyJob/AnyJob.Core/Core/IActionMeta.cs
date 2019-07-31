
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionMeta
    {
        string Kind { get;}
        string Description { get; }
        string EntryPoint { get; }
        bool Enabled { get; }
        IReadOnlyList<string> Tags { get; }
        IDictionary<string, IActionType> Inputs { get; }
        IActionType Output { get; }
    }
}
