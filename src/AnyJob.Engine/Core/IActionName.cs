namespace AnyJob
{
    /// <summary>
    /// 表示动作的结构化名称
    /// </summary>
    public interface IActionName
    {

        /// <summary>
        /// 表示动作所属的提供者
        /// </summary>
        string Provider { get; }
        /// <summary>
        /// 表示动作的名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 表示动作所属的包
        /// </summary>
        string Pack { get; }
        /// <summary>
        /// 表示动作所属的版本
        /// </summary>
        string Version { get; }
    }
}
