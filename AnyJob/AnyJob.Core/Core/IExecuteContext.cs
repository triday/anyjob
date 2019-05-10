using System;
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
        IActionParameters ActionParameters { get; }
        /// <summary>
        /// 或者执行的Action的Ref名称
        /// </summary>
        string ActionRef { get; }
        /// <summary>
        /// 获取执行的Spy，用于取消执行
        /// </summary>
        IExecuteSpy ExecuteSpy { get; set; }
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
        /// <summary>
        /// 获取执行的深度
        /// </summary>
        int ExecutionDepth { get;  }
        /// <summary>
        /// 表示失败重试次数
        /// </summary>
        int ActionRetryCount { get; }
    }
}