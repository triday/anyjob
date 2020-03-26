using System.Collections.Generic;

namespace AnyJob
{
    /// <summary>
    /// 表达动作的元数据信息
    /// </summary>
    public interface IActionMeta
    {
        /// <summary>
        /// 获取动作的类型
        /// </summary>
        string Kind { get;}
        /// <summary>
        /// 获取动作的描述信息
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 获取或设置动作的入口信息
        /// </summary>
        string EntryPoint { get; }
        /// <summary>
        /// 获取一个值，表示动作是否启用
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// 获取动作关联的标签项
        /// </summary>
        IReadOnlyList<string> Tags { get; }
        /// <summary>
        /// 获取动作的输入参数列表
        /// </summary>
        IDictionary<string, IActionType> Inputs { get; }
        /// <summary>
        /// 获取动作的输出参数
        /// </summary>
        IActionType Output { get; }
    }
}
