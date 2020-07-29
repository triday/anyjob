using System;

namespace AnyJob.Runner.Process
{
    [Serializable]
    public class TypedProcessException : ActionException
    {
        private readonly TypedError typedError;
        public TypedProcessException(TypedError typedError)
        {
            this.typedError = typedError;
        }

        public TypedProcessException()
        {
        }

        public TypedProcessException(string message) : base(message)
        {
        }

        public TypedProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypedProcessException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
        }
    }
}
