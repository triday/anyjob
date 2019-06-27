using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AnyJob
{
    public interface IActionParameters
    {
        IDictionary<string, object> Context { get; }
        IDictionary<string, object> Inputs { get; }
    }
}