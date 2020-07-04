using System.Threading;

namespace AnyJob
{
    /// <summary>
    /// 表示一次执行的上下文环境
    /// </summary>
    public interface IExecuteContext
    {
        /// <summary>
        /// 获取Action的执行参数
        /// </summary>
        IExecuteParameter ExecuteParameter { get; }
        /// <summary>
        /// 或者执行的Action的名称
        /// </summary>
        string ActionFullName { get; }
        /// <summary>
        /// 获取取消执行的Token
        /// </summary>
        CancellationToken Token { get; }
        /// <summary>
        /// 获取执行的路径
        /// </summary>
        IExecutePath ExecutePath { get; }
        /// <summary>
        /// 表示失败重试次数
        /// </summary>
        int ActionRetryCount { get; }
        /// <summary>
        /// 表示执行的名称
        /// </summary>
        string ExecuteName { get; }

    }
}
