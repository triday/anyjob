using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的描述
    /// </summary>
    public interface IActionDesc:IActionMeta
    {
        /// <summary>
        /// 根据运行参数创建Action的实例
        /// </summary>
        /// <param name="parameters">Action的执行参数</param>
        /// <returns>返回Action实例</returns>
        IAction CreateInstance(ActionParameters parameters);
    }


}
