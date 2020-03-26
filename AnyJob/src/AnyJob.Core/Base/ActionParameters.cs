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

        public IDictionary<string, object> GlobalVars { get; set; }

        public IDictionary<string, object> Arguments { get; set; }

        public IDictionary<string, object> GetAllValues()
        {
            Dictionary<string, object> allValues = new Dictionary<string, object>();
            MegerDic(Context, allValues);
            MegerDic(Arguments, allValues);
            MegerDic(GlobalVars, allValues);
            MegerDic(Vars, allValues);
            return allValues;
        }
        private void MegerDic(IDictionary<string, object> source, IDictionary<string, object> target)
        {
            if (source == null) return;
            foreach (var kv in source)
            {
                target.Add(kv);
            }
        }
    }
}
