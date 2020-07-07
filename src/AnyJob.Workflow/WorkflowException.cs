using System;
using System.Collections.Generic;
using System.Text;

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
