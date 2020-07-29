using System;

namespace AnyJob.Runner.Workflow
{
    [Serializable]
    public class WorkflowException : ActionException
    {
        public WorkflowException()
        {
        }
        public WorkflowException(string errorCode, string message) : base(errorCode, message)
        {

        }
        public WorkflowException(string errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
        {
        }

        public WorkflowException(string message) : base(message)
        {
        }

        public WorkflowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkflowException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
        }
    }
}
