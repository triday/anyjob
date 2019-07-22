using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ExecuteParameter : IExecuteParameter
    {
        public IDictionary<string, object> Context { get; set; }

        public IDictionary<string, object> Inputs { get; set; }

        public IDictionary<string, object> Vars { get; set; }

        public IDictionary<string, object> GlobalVars { get; set; }
    }
}
