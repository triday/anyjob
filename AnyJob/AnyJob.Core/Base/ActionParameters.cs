using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的执行参数
    /// </summary>
    public class ActionParameters : IActionParameter
    {

        public IDictionary<string, object> Context { get; set; }
        
        public IDictionary<string, object> Vars { get; set; }

        public IDictionary<string, object> Outputs { get; set; }

        public IDictionary<string, object> Arguments { get; set; }


    }
}
