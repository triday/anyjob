using AnyJob.Workflow.Models;
using AnyJob.Workflow.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace AnyJob.Workflow.Impl
{
    public class DefaultGroupRunnerService : IGroupRunnerService
    {
        IActionExecuterService actionExecuterService;
        public Task RunGroup(ActionContext actionContext, GroupInfo groupInfo)
        {
            if (groupInfo == null)
            {
                return Task.CompletedTask;
            }
            if (groupInfo.Entry != null && groupInfo.Entry.Length == 0)
            {
                return Task.CompletedTask;
            }
            this.PublishGroupVars(actionContext, groupInfo);
            var tasks = GetRunTasks(string.Empty, groupInfo.Entry, groupInfo);
            return RunTaskInfos(actionContext, tasks);
        }

        private void PublishGroupVars(ActionContext actionContext, GroupInfo groupInfo)
        {

        }
        private List<TaskDesc> GetRunTasks(string fromName, IEnumerable<string> taskNames, GroupInfo groupInfo)
        {
            if (taskNames == null) return new List<TaskDesc>();
            return taskNames.Select(taskName => FindTaskInfo(fromName, taskName, groupInfo)).ToList();
        }
        private TaskDesc FindTaskInfo(string fromName, string taskName, GroupInfo groupInfo)
        {
            if (groupInfo.Tasks == null || !groupInfo.Tasks.ContainsKey(taskName))
            {
                throw WorkflowError.TaskNameNotFound(taskName);
            }
            return new TaskDesc()
            {
                FromName = fromName,
                TaskName = taskName,
                TaskInfo = groupInfo.Tasks[taskName]
            };
        }

        protected Task RunTaskInfos(ActionContext actionContext, IEnumerable<TaskDesc> taskDescs)
        {
            var allTasks = taskDescs.Select(p => RunTaskInfo(p)).ToArray();
            return Task.WhenAll(allTasks);
        }

        protected Task RunTaskInfo(TaskDesc taskInfo)
        {
            IExecuteContext executeContext = new ExecuteContext();
            return actionExecuterService.Execute(executeContext).ContinueWith((result) =>
            {
                //publish output

                //getnext tasks;
                Task.WaitAll();
            });

        }


        public class TaskDesc
        {
            public string TaskName { get; set; }
            public string FromName { get; set; }
            public TaskInfo TaskInfo { get; set; }
        }
    }
}
