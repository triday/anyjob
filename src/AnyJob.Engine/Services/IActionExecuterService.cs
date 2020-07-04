using System.Threading.Tasks;

namespace AnyJob
{
    /// <summary>
    /// 表示任务执行的服务
    /// </summary>
    public interface IActionExecuterService
    {
        /// <summary>
        /// 执行一次任务
        /// </summary>
        /// <param name="executeContext">任务上下文</param>
        /// <returns>返回<see cref="Task"/>对象</returns>
        Task<ExecuteResult> Execute(IExecuteContext executeContext);
    }


}
