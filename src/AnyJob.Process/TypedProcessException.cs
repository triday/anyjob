using System;
using System.Collections.Generic;
using System.Text;

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
