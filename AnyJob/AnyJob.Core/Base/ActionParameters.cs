using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的执行参数
    /// </summary>
    public class ActionParameters : IActionParameters
    {
        public ActionParameters(IDictionary<string, object> inputs = null, IDictionary<string, object> context = null)
        {
            if (inputs != null)
            {
                this.Inputs = new ConcurrentDictionary<string, object>(inputs);
            }
            if (context != null)
            {
                this.Context = new ConcurrentDictionary<string, object>(context);
            }
        }

        public IDictionary<string, object> Inputs { get; private set; } = new ConcurrentDictionary<string, object>();

        public IDictionary<string, object> Context { get; private set; } = new ConcurrentDictionary<string, object>();

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
