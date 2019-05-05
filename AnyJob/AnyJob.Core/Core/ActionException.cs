using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的异常
    /// </summary>
    [Serializable]
    public class ActionException : Exception
    {
        public ActionException() { }
        public ActionException(string message) : base(message) { }
        public ActionException(string message, Exception inner) : base(message, inner) { }
    }
}
