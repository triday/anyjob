namespace AnyJob
{
    /// <summary>
    /// 表示Action的接口
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 运行Action
        /// </summary>
        /// <param name="context">提供Action的执行上下文环境</param>
        /// <returns></returns>
        object Run(IActionContext context);
    }
}
