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
    }
}
