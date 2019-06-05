using System;

namespace AnyJob
{
    public interface IExecuteResult
    {
        Exception Error { get; set; }
        bool IsSuccess { get; }
        object Result { get; set; }
    }
}