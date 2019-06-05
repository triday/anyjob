using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示任务执行的状态
    /// </summary>
    public enum ExecuteState
    {
        Requested=0,
        Ready=1,
        Running=2,
        Success=3,
        Failure=4,
    }
}
