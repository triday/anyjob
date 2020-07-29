
using System;
using System.Threading;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的运行上下文环境
    /// </summary>
    public interface IActionContext
    {
        IActionName Name { get; }
        /// <summary>
        /// 获取Action的元数据信息
        /// </summary>
        IActionMeta MetaInfo { get; }
        /// <summary>
        /// 获取运行时的信息
        /// </summary>
        IActionRuntime RuntimeInfo { get; }
        /// <summary>
        /// 获取Action的执行参数
        /// </summary>
        IActionParameter Parameters { get; }
        /// <summary>
        /// 获取取消任务的Token
        /// </summary>
        CancellationToken Token { get; }
        /// <summary>
        /// 获取任务执行的路径信息
        /// </summary>
        IExecutePath ExecutePath { get; }
        /// <summary>
        /// 获取任务执行的名称
        /// </summary>
        string ExecuteName { get; }
        /// <summary>
        /// 获取服务提供商
        /// </summary>
        IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 获取执行过程中的日志记录器
        /// </summary>
        IActionLogger Output { get; }
        /// <summary>
        /// 获取执行过程中的错误信息
        /// </summary>
        IActionLogger ExecuteError { get; }

    }
}
