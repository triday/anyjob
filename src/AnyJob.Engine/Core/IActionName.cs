namespace AnyJob
{
    /// <summary>
    /// 表示动作的结构化名称
    /// </summary>
    public interface IActionName
    {
        /// <summary>
        /// 表示动作的名称
        /// </summary>
        string Name { get; }
        string Pack { get; }
        string Version { get; }
    }
}
