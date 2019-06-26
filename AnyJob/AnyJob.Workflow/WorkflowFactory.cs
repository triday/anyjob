using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.DependencyInjection;
using AnyJob.Impl;
namespace AnyJob.Workflow
{
    [ServiceImplClass(typeof(IActionFactoryService))]
    public class WorkflowFactory : IActionFactoryService
    {

        public string ActionKind => throw new NotImplementedException();

        public IAction CreateAction(IActionContext actionContext)
        {
            return null;
        }
    }
}
