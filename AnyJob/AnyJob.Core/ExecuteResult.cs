using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的执行结果
    /// </summary>
    public class ExecuteResult
    {
        public ExecuteResult()
        {

        }
        public ExecuteResult(object result) : this(result, true)
        {
        }
        public ExecuteResult(object result, bool success)
        {
            this.Result = result;
            this.IsSuccess = success;
        }
        /// <summary>
        /// 执行过程的错误
        /// </summary>
        public Exception Error { get; set; }
        /// <summary>
        /// Action的运行结果
        /// </summary>
        public object Result { get; set; }

        public bool IsSuccess { get; set; }

     

    }

}
