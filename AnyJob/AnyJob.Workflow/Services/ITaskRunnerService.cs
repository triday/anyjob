using AnyJob.Workflow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Workflow.Services
{
    public interface ITaskRunnerService
    {
        Task RunTask(IActionContext actionContext, TaskDesc taskDesc, GroupInfo groupInfo);
    }
}
