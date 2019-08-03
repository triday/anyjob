using System.Collections.Generic;

namespace AnyJob
{
    /// <summary>
    /// 表达动作的元数据信息
    /// </summary>
    public interface IActionMeta
    {
        string Kind { get;}
        string Description { get; }
        string EntryPoint { get; }
        bool Enabled { get; }
        IReadOnlyList<string> Tags { get; }
        IDictionary<string, IActionType> Inputs { get; }
        IActionType Output { get; }
    }
}
