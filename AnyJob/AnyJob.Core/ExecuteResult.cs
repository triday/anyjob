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
        /// <summary>
        /// 获取或设置执行过程的错误
        /// </summary>
        public Exception Error { get; set; }
        /// <summary>
        /// Action的运行结果
        /// </summary>
        public object Result { get; set; }
        /// <summary>
        /// 获取是否执行成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return this.Error == null;
            }
        }
        /// <summary>
        /// 获取是否被取消
        /// </summary>
        public bool IsCanceled
        {
            get
            {
                return this.Error != null;
            }
        }
        /// <summary>
        /// 获取是否超时
        /// </summary>
        public bool IsTimeout
        {
            get
            {
                return this.Error != null; 
            }
        }

    }

}
