using AnyJob.Meta;
using System;

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
        IActionParameters Parameters { get; }
    }
}