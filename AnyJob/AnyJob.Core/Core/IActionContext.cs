using AnyJob.Meta;
using System;
using System.Threading;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的运行上下文环境
    /// </summary>
    public interface IActionContext : IServiceProvider
    {
        /// <summary>
        /// 获取Action的元数据信息
        /// </summary>
        IActionMeta MetaInfo { get; }
        /// <summary>
        /// 获取Action的执行参数
        /// </summary>
        ActionParameters Parameters { get; }
        /// <summary>
        /// 获取取消任务的Token
        /// </summary>
        CancellationToken Token { get; }
        /// <summary>
        /// 获取任务执行的路径信息
        /// </summary>
        IExecutePath ExecutePath { get; }
    }
}