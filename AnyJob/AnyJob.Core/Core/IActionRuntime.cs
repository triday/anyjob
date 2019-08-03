using System.Runtime.InteropServices;

namespace AnyJob
{
    /// <summary>
    /// 表示动作的运行时信息
    /// </summary>
    public interface IActionRuntime
    {
        /// <summary>
        /// 获取动作执行时的工作目录
        /// </summary>
        string WorkingDirectory { get; }
        /// <summary>
        /// 获取动作执行时的操作系统信息
        /// </summary>
        OSPlatform OSPlatForm { get; set; }
        //string NetCoreVersion { get; set; }

    }

}
