using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.DependencyInjection;
using AnyJob.Impl;
using AnyJob.Workflow.Config;
using AnyJob.Workflow.Models;
using Microsoft.Extensions.Options;

namespace AnyJob.Workflow
{
    [ServiceImplClass(Key = "workflow")]
    public class WorkflowFactoryService : IActionFactoryService
    {
        IFileStoreService fileStoreService;
        public WorkflowFactoryService(IFileStoreService fileStoreService)
        {
            this.fileStoreService = fileStoreService;
        }


        public IAction CreateAction(IActionContext actionContext)
        {
            string workingDir = actionContext.RuntimeInfo.WorkingDirectory;
            string entryPoint = actionContext.MetaInfo.EntryPoint;
            string entryFile = System.IO.Path.Combine(workingDir, entryPoint);
            var workflowInfo = fileStoreService.ReadObject<WorkflowInfo>(entryFile);
            return new WorkflowAction(workflowInfo);
        }
    }
}
