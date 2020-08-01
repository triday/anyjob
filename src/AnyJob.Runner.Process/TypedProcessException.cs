using System;

namespace AnyJob.Runner.Process
{
    [Serializable]
    public class TypedProcessException : ActionException
    {
        public TypedError Error { get; }
        public TypedProcessException(TypedError typedError) : base(typedError.Message)
        {
            this.Error = typedError;
        }

        public TypedProcessException()
        {
        }
    }
}
