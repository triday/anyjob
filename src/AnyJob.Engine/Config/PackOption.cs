using System.Collections.Generic;
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
        public string RootDir { get; set; } = "actions";

        public string DefaultProviderName { get; set; } = "default";
        public IDictionary<string, string> Providers { get; set; } = new Dictionary<string, string>()
        {
            ["default"] = "http://localhost"
        };

        public string DownLoadCacheDir { get; set; } = "downloads";
    }
}
