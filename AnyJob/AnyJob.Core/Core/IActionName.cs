namespace AnyJob
{
    /// <summary>
    /// 表示动作的结构化名称
    /// </summary>
    public interface IActionName
    {
        string Name { get;  }
        string Pack { get; }
        string Version { get; }
    }
}
