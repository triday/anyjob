using System.Collections.Generic;

namespace AnyJob
{
    /// <summary>
    /// 表示执行的路径信息
    /// </summary>
    public interface IExecutePath
    {
        /// <summary>
        /// 获取执行的路径的深度 
        /// </summary>
        int Depth { get; }
        /// <summary>
        /// 获取执行的ID
        /// </summary>
        string ExecuteId { get; }
        /// <summary>
        /// 获取执行的父ID
        /// </summary>
        string ParentId { get; }
        /// <summary>
        /// 获取执行的路径列表
        /// </summary>
        IReadOnlyList<string> Paths { get; }
        /// <summary>
        /// 获取执行的根ID
        /// </summary>
        string RootId { get; }
        /// <summary>
        /// 根据子执行ID创建一个新的路径信息
        /// </summary>
        /// <param name="subExecuteId">子执行ID</param>
        /// <returns>返回新的路径信息</returns>
        IExecutePath NewSubPath(string subExecuteId);
    }
}