using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示Action的异常
    /// </summary>
    [Serializable]
    public class ActionException : Exception
    {
        public ActionException()
        {
        }
        public ActionException(string errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public ActionException(string errorCode, string message, Exception inner) : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }

        public string ErrorCode { get; private set; }

        public static ActionException FromErrorCode(string errorCode, params object[] args)
        {
            string messageFormat = ErrorCodes.ResourceManager.GetString(errorCode, ErrorCodes.Culture);
            string message = string.Format(messageFormat, args);
            return new ActionException(errorCode, message);
        }
        public static ActionException FromErrorCode(Exception exception, string errorCode, params object[] args)
        {
            string messageFormat = ErrorCodes.ResourceManager.GetString(errorCode, ErrorCodes.Culture);
            string message = string.Format(messageFormat, args);
            return new ActionException(errorCode, message, exception);
        }
    }
}
