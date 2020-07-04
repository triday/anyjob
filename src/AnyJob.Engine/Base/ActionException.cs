using System;

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
        /// <summary>
        /// 获取异常编码
        /// </summary>
        public string ErrorCode { get; private set; }

        public ActionException(string message) : base(message)
        {
        }

        public ActionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActionException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {

        }
    }
}
