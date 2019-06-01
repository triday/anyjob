using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionMeta:IActionName
    {
        string ActionKind { get;}
        string Description { get; }
        string EntryPoint { get; }
        bool Enabled { get; }
    }
}
