using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的执行参数
    /// </summary>
    public class IActionParameters
    {
        public IActionParameters(Dictionary<string, object> inputs = null, Dictionary<string, object> context = null)
        {
            if (inputs != null)
            {
                this.Inputs = inputs;
            }
            if (context != null)
            {
                this.Context = context;
            }
        }

        public Dictionary<string, object> Inputs { get; private set; } = new Dictionary<string, object>();

        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        public void AddInputValue(string name, object value)
        {
            this.Inputs.Add(name, value);
        }

        public void AddContextValue(string name, object value)
        {
            this.Context.Add(name, value);
        }
    }
}
