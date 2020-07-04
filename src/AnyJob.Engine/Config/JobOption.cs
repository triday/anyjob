using System.ComponentModel.DataAnnotations;
using AnyJob.DependencyInjection;
using YS.Knife;
namespace AnyJob.Config
{
    /// <summary>
    /// 表示任务的配置项
    /// </summary>
    [OptionsClass("pack")]
    public class JobOption
    {
        /// <summary>
        /// 获取或设置最大任务数量
        /// </summary>
        public int MaxJobCount { get; set; }
    }
}
