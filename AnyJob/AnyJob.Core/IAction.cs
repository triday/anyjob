using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的接口
    /// </summary>
    public interface IAction
    {
        object Run(ActionContext context);
    }
}
