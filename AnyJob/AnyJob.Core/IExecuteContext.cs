using System;
using System.Threading;

namespace AnyJob
{
    /// <summary>
    /// 表示一次执行的上下文环境
    /// </summary>
    public interface IExecuteContext
    {

        IActionParameters ActionParameters { get; }
        /// <summary>
        /// 或者执行的Action的Ref名称
        /// </summary>
        string ActionRef { get; }
        CancellationTokenSource CancelTokenSource { get; }
        /// <summary>
        /// 获取执行的Id
        /// </summary>
        string ExecutionId { get; }
        /// <summary>
        /// 获取执行的父Id
        /// </summary>
        string ParentExecutionId { get; }
        /// <summary>
        /// 获取执行的根Id
        /// </summary>
        string RootExecutionId { get; }
    }
}