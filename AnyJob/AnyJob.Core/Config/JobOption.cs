using AnyJob.DependencyInjection;

namespace AnyJob.Config
{
    /// <summary>
    /// 表示任务的配置项
    /// </summary>
    [ConfigClass("pack")]
    public class JobOption
    {
        /// <summary>
        /// 获取或设置最大任务数量
        /// </summary>
        public int MaxJobCount { get; set; }
    }
}
