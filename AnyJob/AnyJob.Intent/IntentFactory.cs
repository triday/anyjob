using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
using AnyJob.Impl;
namespace AnyJob.Intent
{
    [ServiceImplClass(typeof(IActionFactory))]
    public class IntentFactory : IActionFactory
    {
        public int Priority => throw new NotImplementedException();

        public IActionEntry GetEntry(string refName)
        {
            throw new NotImplementedException();
        }
    }
}
