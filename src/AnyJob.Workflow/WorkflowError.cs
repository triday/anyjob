using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow
{
    public sealed class WorkflowError
    {
        /// <summary>
        /// 找不到对应名称的task
        /// </summary>
        public static WorkflowException TaskNameNotFound(string taskName)
        {
            return FromErrorMessage(nameof(ErrorMessage.E70001), taskName);
        }

        public static WorkflowException TaskEexcuteError(string taskName, Exception exception)
        {
            return FromErrorMessage(nameof(ErrorMessage.E70002), taskName, exception);
        }


        private static WorkflowException FromErrorMessage(string name, params object[] args)
        {
            string format = ErrorMessage.ResourceManager.GetString(name);
            string message = string.Format(format, args);
            return new WorkflowException(name, message);
        }
        private static WorkflowException FromErrorMessage(string name, Exception exception, params object[] args)
        {
            string format = ErrorMessage.ResourceManager.GetString(name);
            string message = string.Format(format, args);
            return new WorkflowException(name, message, exception);
        }
    }
}
