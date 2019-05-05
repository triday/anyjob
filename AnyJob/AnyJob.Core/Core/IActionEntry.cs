using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的入口信息
    /// </summary>
    public interface IActionEntry
    {
        /// <summary>
        /// 获取Action的元数据信息
        /// </summary>
        IActionMeta MetaInfo { get; }
        /// <summary>
        /// 根据运行参数创建Action的实例
        /// </summary>
        /// <param name="parameters">Action的执行参数</param>
        /// <returns>返回Action实例</returns>
        IAction CreateInstance(IActionParameters parameters);
    }
}
