using YS.Knife;
namespace AnyJob.Config
{
    /// <summary>
    /// 表示Pack的配置项
    /// </summary>
    [OptionsClass("pack")]
    public class PackOption
    {
        /// <summary>
        /// 获取或设置根目录(相对或绝对)
        /// </summary>
        public string RootDir { get; set; } = "packs";

    }
}
