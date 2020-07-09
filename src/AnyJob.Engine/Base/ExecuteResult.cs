using System;

namespace AnyJob
{
    /// <summary>
    /// 表示执行结果
    /// </summary>
    public class ExecuteResult : IExecuteResult
    {
        /// <summary>
        /// 获取或设置执行过程的错误
        /// </summary>
        public Exception ExecuteError { get; set; }
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
                return this.ExecuteError == null;
            }
        }

        public static ExecuteResult FromError(Exception error)
        {
            return new ExecuteResult()
            {
                ExecuteError = error
            };
        }
        public static ExecuteResult FromResult(object result)
        {
            return new ExecuteResult()
            {
                Result = result
            };
        }
    }

}
