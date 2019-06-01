using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的定义
    /// </summary>
    public interface IActionDefination
    {
        /// <summary>
        /// 表示Action的输入定义
        /// </summary>
        List<IActionInputDefination> Inputs { get; }
        /// <summary>
        /// 表示Action的输出定义
        /// </summary>
        IActionOutputDefination Output { get; }
        /// <summary>
        /// 根据运行参数创建Action的实例
        /// </summary>
        /// <param name="parameters">Action的执行参数</param>
        /// <returns>返回Action实例</returns>
        IAction CreateInstance(ActionParameters parameters);
    }


}
