using System;

namespace AnyJob
{
    /// <summary>
    /// 表示执行的结果
    /// </summary>
    public interface IExecuteResult
    {
        Exception Error { get; set; }
        bool IsSuccess { get; }
        object Result { get; set; }
    }
}
