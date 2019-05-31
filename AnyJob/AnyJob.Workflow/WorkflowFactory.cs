using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
using AnyJob.Impl;
namespace AnyJob.Workflow
{
    [ServiceImplClass(typeof(IActionDescFactory))]
    public class WorkflowFactory : IActionDescFactory
    {
        public int Priority => 1000;

        public IActionDesc GetEntry(string refName)
        {
            throw new NotImplementedException();
        }
    }
}
