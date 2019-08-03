namespace AnyJob
{
    /// <summary>
    /// 表示获取动作结构化名称的服务
    /// </summary>
    public interface IActionNameResolveService
    {
        /// <summary>
        /// 获取动作的结构化名称
        /// </summary>
        /// <param name="fullName">动作的全名称</param>
        /// <returns>返回动作的结构化名称</returns>
        IActionName ResolverName(string fullName);
    }
}
