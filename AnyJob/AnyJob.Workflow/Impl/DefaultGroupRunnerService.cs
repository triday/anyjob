using AnyJob.DependencyInjection;
using AnyJob.Workflow.Config;
using AnyJob.Workflow.Models;
using AnyJob.Workflow.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AnyJob.Workflow.Impl
{
    [ServiceImplClass]
    public class DefaultGroupRunnerService : IGroupRunnerService
    {
        IActionExecuterService actionExecuterService;
        IPublishValueService publishValueService;
        WorkflowOption workflowOption;
        IIdGenService idGenService;
        IDynamicValueService dynamicValueService;
        IConvertService convertService;

        public DefaultGroupRunnerService(IActionExecuterService actionExecuterService, IPublishValueService publishValueService, IIdGenService idGenService, IDynamicValueService dynamicValueService, IConvertService convertService,IOptions<WorkflowOption> workflowOption)
        {
            this.actionExecuterService = actionExecuterService;
            this.publishValueService = publishValueService;
            this.idGenService = idGenService;
            this.dynamicValueService = dynamicValueService;
            this.convertService = convertService;
            this.workflowOption = workflowOption.Value;
        }


        public virtual Task RunGroup(IActionContext actionContext, GroupInfo groupInfo)
        {
            if (groupInfo == null) return Task.CompletedTask;
            publishValueService.PublishVars(actionContext.Parameters, groupInfo.Vars);
            var tasks = GetRunTasks(string.Empty, groupInfo.Entry, groupInfo);
            return RunTasks(actionContext, tasks, groupInfo);
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
        private Task RunTasks(IActionContext actionContext, IEnumerable<TaskDesc> taskDescs, GroupInfo groupInfo)
        {
            var allTasks = taskDescs.Select(p => RunTask(actionContext, p, groupInfo)).ToArray();
            return Task.WhenAll(allTasks);
        }
        private Task RunTask(IActionContext actionContext, TaskDesc taskDesc, GroupInfo groupInfo)
        {
            bool isSubEntryAction = this.IsSubEntryGroup(taskDesc.TaskInfo.Action);
            if (!isSubEntryAction) {
                publishValueService.PublishVars(actionContext.Parameters, taskDesc.TaskInfo.Vars);
            }
            IExecuteContext executeContext = CreateExecuteContext(actionContext, taskDesc,isSubEntryAction);
            return actionExecuterService.Execute(executeContext).ContinueWith((result) =>
            {
                if (result.IsCompletedSuccessfully)
                {
                    var executeResult = result.Result;
                    this.PublishResultVars(actionContext, executeResult, taskDesc);
                    PublishGlobalVars(actionContext, executeResult.IsSuccess, taskDesc);
                    List<TaskDesc> nextTasks = GetNextTasks(actionContext, executeResult.IsSuccess, taskDesc, groupInfo);
                    RunTasks(actionContext, nextTasks, groupInfo).Wait();
                }
                else
                {
                    throw WorkflowError.TaskEexcuteError(taskDesc.TaskName, result.Exception);
                }
            }, actionContext.Token);
        }

        
     


        private void PublishResultVars(IActionContext actionContext, IExecuteResult result, TaskDesc taskDesc)
        {
            publishValueService.PublishVars($"{taskDesc.TaskName}_result", result.Result, actionContext.Parameters);
            publishValueService.PublishVars($"{taskDesc.TaskName}_error", result.Error, actionContext.Parameters);
            //publishValueService.PublishVar($"{taskDesc.TaskName}_log", result., actionContext.Parameters);

        }
        private void PublishGlobalVars(IActionContext actionContext, bool success, TaskDesc taskDesc)
        {
            if (success)
            {
                PublishGlobalVars(actionContext, taskDesc.TaskInfo.OnSuccess);
            }
            else
            {
                PublishGlobalVars(actionContext, taskDesc.TaskInfo.OnError);
            }

            PublishGlobalVars(actionContext, taskDesc.TaskInfo.OnComplete);
        }
        private void PublishGlobalVars(IActionContext actionContext, TaskChainGroup taskChain)
        {
            if (taskChain == null) return;
            publishValueService.PublishGlobalVars(actionContext.Parameters, taskChain.GlobalVars);
        }
        private List<TaskDesc> GetNextTasks(IActionContext actionContext, bool success, TaskDesc taskDesc, GroupInfo groupInfo)
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
        private IEnumerable<string> GetNextTasksFromNextChain(IActionContext actionContext, TaskChainGroup taskChain)
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
        private bool IsInCondition(IActionContext actionContext, object condition)
        {
            if (condition == null) return true;
            object dynamicValue = dynamicValueService.GetDynamicValue(condition, actionContext.Parameters);
            return (bool)convertService.Convert(dynamicValue, typeof(bool));
        }
        private  IExecuteContext CreateExecuteContext(IActionContext actionContext, TaskDesc taskDesc,bool isSubEntryAction)
        {
            string newid = idGenService.NewId();
            return new ExecuteContext()
            {
                ActionFullName = taskDesc.TaskInfo.Action,
                ExecuteName = taskDesc.TaskName,
                Token = actionContext.Token,
                ActionRetryCount = taskDesc.TaskInfo.RetryCount,
                ExecutePath = actionContext.ExecutePath.NewSubPath(newid),
                ExecuteParameter = OnCreateExecuteParameter(actionContext, taskDesc,isSubEntryAction)
            };
        }
        protected virtual IExecuteParameter OnCreateExecuteParameter(IActionContext actionContext, TaskDesc taskDesc,bool isSubEntryAction)
        {
            Dictionary<string, object> inputs = new Dictionary<string, object>();
            if (taskDesc.TaskInfo.Inputs != null)
            {
                foreach (var inputEntry in taskDesc.TaskInfo.Inputs)
                {
                    object value = dynamicValueService.GetDynamicValue(inputEntry.Value, actionContext.Parameters);
                    inputs.Add(inputEntry.Key, value);
                }
            }
            var executeParameter = new ExecuteParameter();
            executeParameter.Inputs = new ReadOnlyDictionary<string, object>(inputs);
            executeParameter.Context = actionContext.Parameters.Context;
            executeParameter.GlobalVars = isSubEntryAction ? actionContext.Parameters.GlobalVars : new ConcurrentDictionary<string, object>();
            if (isSubEntryAction)
            {
                var newVars = new ConcurrentDictionary<string, object>(actionContext.Parameters.Vars);
                newVars[workflowOption.SubEntryActionVarName] = taskDesc.TaskInfo;
                executeParameter.Vars = newVars;
            }
            else
            {
                executeParameter.Vars = new ConcurrentDictionary<string, object>();
            }
            return executeParameter;
        }

        private bool IsSubEntryGroup(string actionName)
        {
            if (workflowOption.SubEntryActions == null) return false;
            return workflowOption.SubEntryActions.Contains(actionName);
        }



    }
}
