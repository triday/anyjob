using AnyJob.Workflow.Config;
using AnyJob.Workflow.Models;
using AnyJob.Workflow.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Workflow.Impl
{
    public class DefaultTaskRunnerServcie: ITaskRunnerService
    {
        WorkflowOption workflowOption;

        public Task RunTask(IActionContext actionContext, TaskDesc taskDesc, GroupInfo groupInfo)
        {
            throw new NotImplementedException();
        }

        private bool IsSubEntryGroup(string actionName)
        {
            if (workflowOption.SubEntryActions == null) return false;
            return workflowOption.SubEntryActions.Contains(actionName);
        }

        
    }
}
