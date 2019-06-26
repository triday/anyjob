using System;
using System.Collections.Generic;
using System.Text;
using AnyJob;
using AnyJob.DependencyInjection;
using AnyJob.Impl;

namespace AnyJob.Intent
{
    [ServiceImplClass(typeof(IActionFactoryService))]
    public class LocalIntentActionFactory : IActionFactoryService
    {
        public LocalIntentActionFactory(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
        }
        private ISerializeService serializeService;


        public string ActionKind => "intent";

        public IAction CreateAction(IActionContext actionContext)
        {
            return new IntentAction();
        }
    }
}
