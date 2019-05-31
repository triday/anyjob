using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionEntry
    {
        IActionName ActionName { get; }
        string ActionKind { get;}
        string WorkingDirectory { get; }
        string EntryPoint { get; }
    }
}
