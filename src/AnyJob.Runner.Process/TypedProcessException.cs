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
    }
}
