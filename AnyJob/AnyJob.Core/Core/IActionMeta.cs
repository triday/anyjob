
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionMeta
    {
        string ActionKind { get;}
        string Description { get; }
        string EntryPoint { get; }
        bool Enabled { get; }
        IReadOnlyList<string> Tags { get; }
        IReadOnlyDictionary<string, IActionInputDefination> Inputs { get; }
        IActionOutputDefination Output { get; }
    }
}
