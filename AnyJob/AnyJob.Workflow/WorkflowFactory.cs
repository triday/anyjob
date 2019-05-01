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
        public int Priority => 1000;

        public IActionEntry GetEntry(string refName)
        {
            throw new NotImplementedException();
        }
    }
}
