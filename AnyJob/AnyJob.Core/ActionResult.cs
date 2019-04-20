using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的执行结果
    /// </summary>
    public class ActionResult
    {
        public ActionResult()
        {

        }
        public ActionResult(object result) : this(result, true)
        {
        }
        public ActionResult(object result, bool success)
        {
            this.Result = result;
            this.IsSuccess = success;
        }
        /// <summary>
        /// 执行过程的错误
        /// </summary>
        public Exception Error { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        public object Result { get; set; }

        public bool IsSuccess { get; set; }

     

    }

}
