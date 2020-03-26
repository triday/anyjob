namespace AnyJob
{
    /// <summary>
    /// 表示获取动作的运行时信息的服务
    /// </summary>
    public interface IActionRuntimeService
    {
        /// <summary>
        /// 获取动作的运行时信息
        /// </summary>
        /// <param name="actionName">动作名称</param>
        /// <returns>返回动作的运行时信息</returns>
        IActionRuntime GetRunTime(IActionName actionName);
    }
}
