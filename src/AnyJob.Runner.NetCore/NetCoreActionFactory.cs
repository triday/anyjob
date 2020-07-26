using System;
using System.Linq;
namespace AnyJob.Runner.NetCore
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    [YS.Knife.DictionaryKey("netcore")]
    public class NetCoreActionFactory : IActionFactoryService
    {
        public NetCoreActionFactory(IConvertService convertService)
        {
            this.convertService = convertService;
        }
        private IConvertService convertService;

        public IAction CreateAction(IActionContext actionContext)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            string typeName = actionContext.MetaInfo.EntryPoint;
            var actionType = Type.GetType(typeName);
            var instance = Activator.CreateInstance(actionType);
            SetInputProperties(actionContext, actionType, instance);
            return instance as IAction;
        }
        protected void SetInputProperties(IActionContext actionContext, Type type, object instance)
        {
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            _ = type ?? throw new ArgumentNullException(nameof(type));
            var propsMap = type.GetProperties().ToDictionary(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
            foreach (var kv in actionContext.Parameters.Arguments)
            {
                var prop = propsMap[kv.Key];
                var value = this.convertService.Convert(kv.Value, prop.PropertyType);
                prop.SetValue(instance, value);
            }
        }




    }
}
