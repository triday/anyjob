using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionName
    {
        string Name { get;  }
        string Pack { get; }
        string Version { get; }
    }
}
