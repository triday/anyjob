using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnyJob.Workflow.Models;

namespace AnyJob.Workflow.Services
{
    public interface IGroupRunnerService
    {
        Task RunGroup(IActionContext actionContext, GroupInfo groupInfo);
    }
}
