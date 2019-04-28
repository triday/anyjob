using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
using AnyJob.Impl;
namespace AnyJob.Workflow
{
    [ServiceImplClass(typeof(IActionFactory))]
    public class WorkflowFactory : IActionFactory
    {
        public string ActionType => ConstCode.WORKFLOW_ACTION_TYPE;

        public IAction CreateAction(IActionMeta meta, IActionParameters parameters)
        {
            WorkflowAction action = new WorkflowAction();

            return action;
        }
    }
}
