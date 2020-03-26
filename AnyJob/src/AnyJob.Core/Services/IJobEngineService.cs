namespace AnyJob
{
    /// <summary>
    /// 表示人物引擎服务
    /// </summary>
    public interface IJobEngineService
    {
        /// <summary>
        /// 开始执行一次任务
        /// </summary>
        /// <param name="jobStartInfo">提供任务执行需要的一些参数信息</param>
        /// <returns>返回任务</returns>
        Job Start(JobStartInfo jobStartInfo);
        /// <summary>
        /// 根据任务的执行ID取消执行任务
        /// </summary>
        /// <param name="executionId">任务的执行ID</param>
        /// <returns>取消成功返回<see cref="true"/>，取消失败返回<see cref="false"/>。</returns>
        bool Cancel(string executionId);
    }
}
