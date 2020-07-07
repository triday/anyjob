using System;
using System.Collections.Generic;
using System.Text;
using AnyJob;
using AnyJob.DependencyInjection;
using AnyJob.Impl;

namespace AnyJob.Runner.Intent
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
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
