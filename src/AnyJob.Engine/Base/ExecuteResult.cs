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
        /// 获取或设置执行过程中的日志
        /// </summary>
        public string Logger { get; set; }
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

        public static ExecuteResult FromError(Exception error, string logger)
        {
            return new ExecuteResult()
            {
                ExecuteError = error,
                Logger = logger ?? string.Empty
            };
        }
        public static ExecuteResult FromResult(object result, string logger)
        {
            return new ExecuteResult()
            {
                Result = result,
                Logger = logger ?? string.Empty
            };
        }
    }
}


