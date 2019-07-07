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
        IPublishValueService publishValueService;
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
            return RunTaskInfos(actionContext, tasks, groupInfo);
        }

        private void PublishGroupVars(ActionContext actionContext, GroupInfo groupInfo)
        {

        }
        private List<TaskDesc> GetRunTasks(string fromName, IEnumerable<string> taskNames, GroupInfo groupInfo)
        {
            if (taskNames == null) return new List<TaskDesc>();
            return taskNames.Where(p => !string.IsNullOrEmpty(p)).Select(taskName => FindTaskInfo(fromName, taskName, groupInfo)).Distinct().ToList();
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

        protected Task RunTaskInfos(ActionContext actionContext, IEnumerable<TaskDesc> taskDescs, GroupInfo groupInfo)
        {
            var allTasks = taskDescs.Select(p => RunTaskInfo(actionContext, p, groupInfo)).ToArray();
            return Task.WhenAll(allTasks);
        }

        protected virtual Task RunTaskInfo(ActionContext actionContext, TaskDesc taskDesc, GroupInfo groupInfo)
        {
            IExecuteContext executeContext = OnCreateExecuteContext(actionContext, taskDesc);
            return actionExecuterService.Execute(executeContext).ContinueWith((result) =>
            {
                PublishResultVars(actionContext, result.Result, taskDesc);
                bool success = result.IsCompletedSuccessfully && result.Result.IsSuccess;
                //publish output
                PublishOutputs(actionContext, success, taskDesc);
                List<TaskDesc> nextTasks = GetNextTasks(actionContext, success, taskDesc, groupInfo);
                RunTaskInfos(actionContext, nextTasks, groupInfo).Wait();
            });

        }
        private void PublishResultVars(ActionContext actionContext, IExecuteResult result, TaskDesc taskDesc)
        {
            publishValueService.PublishVar($"{taskDesc.TaskName}_result", result.Result, actionContext.Parameters);
            publishValueService.PublishVar($"{taskDesc.TaskName}_error", result.Error, actionContext.Parameters);
            //publishValueService.PublishVar($"{taskDesc.TaskName}_log", result., actionContext.Parameters);

        }
        private void PublishOutputs(ActionContext actionContext, bool success, TaskDesc taskDesc)
        {
            if (success)
            {
                PublishOutputs(actionContext, taskDesc.TaskInfo.OnSuccess);
            }
            else
            {
                PublishOutputs(actionContext, taskDesc.TaskInfo.OnError);
            }

            PublishOutputs(actionContext, taskDesc.TaskInfo.OnComplete);
        }
        private void PublishOutputs(ActionContext actionContext, TaskChainGroup taskChain)
        {
            if (taskChain == null || taskChain.Outputs == null) return;
            foreach (var kv in taskChain.Outputs)
            {
                publishValueService.PublishOutput(kv.Key, kv.Value, actionContext.Parameters);
            }
        }
        private List<TaskDesc> GetNextTasks(ActionContext actionContext, bool success, TaskDesc taskDesc, GroupInfo groupInfo)
        {
            List<string> allNextTasksNames = new List<string>();
            if (success)
            {
                allNextTasksNames.AddRange(GetNextTasksFromNextChain(actionContext, taskDesc.TaskInfo.OnSuccess));
            }
            else
            {
                allNextTasksNames.AddRange(GetNextTasksFromNextChain(actionContext, taskDesc.TaskInfo.OnError));
            }
            allNextTasksNames.AddRange(GetNextTasksFromNextChain(actionContext, taskDesc.TaskInfo.OnComplete));
            return GetRunTasks(taskDesc.TaskName, allNextTasksNames, groupInfo);
        }
        private IEnumerable<string> GetNextTasksFromNextChain(ActionContext actionContext, TaskChainGroup taskChain)
        {
            if (taskChain == null || taskChain.Chains == null)
            {
                return Enumerable.Empty<string>();
            }
            List<string> nexts = new List<string>();
            foreach (var chain in taskChain.Chains)
            {
                if (chain == null) continue;
                bool inCondition = IsInCondition(actionContext, chain.Condition);
                if (inCondition)
                {
                    nexts.Add(chain.Task);
                    if (chain.Stop)
                    {
                        break;
                    }
                }
            }
            return nexts;

        }
        private bool IsInCondition(ActionContext actionContext, object condition)
        {
            //TODO 
            return true;
        }
        protected virtual IExecuteContext OnCreateExecuteContext(ActionContext actionContext, TaskDesc taskInfo)
        {
            return new ExecuteContext()
            {

            };
        }

        protected class TaskDesc
        {
            public string TaskName { get; set; }
            public string FromName { get; set; }
            public TaskInfo TaskInfo { get; set; }
        }
    }
}
