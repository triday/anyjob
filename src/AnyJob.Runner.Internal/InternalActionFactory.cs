using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob.Runner.Internal
{

    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    [YS.Knife.DictionaryKey("internal")]
    public class InternalActionFactory : IActionFactoryService
    {
        public InternalActionFactory(IConvertService convertService)
        {
            this.convertService = convertService;
        }
        private readonly IConvertService convertService;

        public IAction CreateAction(IActionContext actionContext)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            var actionType = Type.GetType(actionContext.MetaInfo.EntryPoint, true);

            var instance = ActivatorUtilities.CreateInstance(actionContext.ServiceProvider, actionType);
            SetInputProperties(actionContext, actionType, instance);
            return instance as IAction;
        }

        protected void SetInputProperties(IActionContext actionContext, Type type, object instance)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            _ = type ?? throw new ArgumentNullException(nameof(type));
            var propsMap = type.GetProperties().Where(p => p.CanWrite).ToDictionary(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
            foreach (var kv in actionContext.Parameters.Arguments)
            {
                var prop = propsMap[kv.Key];
                var value = this.convertService.Convert(kv.Value, prop.PropertyType);
                prop.SetValue(instance, value);
            }
        }
    }
}
