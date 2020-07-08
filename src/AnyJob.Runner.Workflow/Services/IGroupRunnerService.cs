using System.Threading.Tasks;
using AnyJob.Runner.Workflow.Models;

namespace AnyJob.Runner.Workflow.Services
{
    public interface IGroupRunnerService
    {
        Task RunGroup(IActionContext actionContext, GroupInfo groupInfo);
    }
}
