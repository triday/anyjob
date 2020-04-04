namespace AnyJob
{
    /// <summary>
    /// 表示任务的工厂服务
    /// </summary>
    public interface IActionFactoryService
    {
        /// <summary>
        /// 新创建一个任务对象
        /// </summary>
        /// <param name="actionContext">动作运行上下文</param>
        /// <returns>返回新创建的<see cref="IAction"/>对象</returns>
        IAction CreateAction(IActionContext actionContext);
    }
}
