using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Core
{
    [Serializable]
    public class ExecuteException: ActionException
    {
        public ExecuteException(string errorCode,Exception exception):base(ExecuteErrorCode.ResourceManager.GetString(errorCode),exception)
        {
            this.ErrorCode = errorCode;
        }
        public string ErrorCode { get; private set; }

    }
}
