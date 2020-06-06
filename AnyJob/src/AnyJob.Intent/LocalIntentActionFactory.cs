using System;
using System.Collections.Generic;
using System.Text;
using AnyJob;
using AnyJob.DependencyInjection;
using AnyJob.Impl;

namespace AnyJob.Intent
{
    [AnyJobService(Key ="intent")]
    [YS.Knife.DictionaryKey("intent")]
    public class LocalIntentActionFactory : IActionFactoryService
    {
        public LocalIntentActionFactory(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
        }
        private ISerializeService serializeService;



        public IAction CreateAction(IActionContext actionContext)
        {
            return new IntentAction();
        }
    }
}
