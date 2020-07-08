using System.Collections.Generic;

namespace AnyJob
{
    public interface IExecuteParameter
    {
        IDictionary<string, object> Context { get; }
        IDictionary<string, object> Inputs { get; }
        IDictionary<string, object> Vars { get; }
        IDictionary<string, object> GlobalVars { get; }
    }
}
