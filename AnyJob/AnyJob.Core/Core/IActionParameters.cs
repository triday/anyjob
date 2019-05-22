using System.Collections.Generic;

namespace AnyJob
{
    public interface IActionParameters
    {
        Dictionary<string, object> Context { get; }
        Dictionary<string, object> Inputs { get; }
    }
}