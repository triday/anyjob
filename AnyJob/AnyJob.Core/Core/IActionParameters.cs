using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AnyJob
{
    public interface IActionParameter
    {
        IDictionary<string, object> Context { get; }
        IDictionary<string, object> Arguments { get; }
        IDictionary<string,object> Vars { get; }
        IDictionary<string,object> Outputs { get; }
    }
}