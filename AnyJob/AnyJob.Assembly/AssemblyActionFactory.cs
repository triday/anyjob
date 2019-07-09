using AnyJob.DependencyInjection;
using AnyJob.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AnyJob.Assembly
{
    [ServiceImplClass(typeof(IActionFactoryService))]
    public class AssemblyActionFactory : IActionFactoryService
    {
        public AssemblyActionFactory(IConvertService convertService)
        {
            this.convertService = convertService;
        }
        private IConvertService convertService;
        public string ActionKind => "assembly";

        public IAction CreateAction(IActionContext actionContext)
        {
            string typeName = actionContext.MetaInfo.EntryPoint;
            var actionType = Type.GetType(typeName);
            var instance = Activator.CreateInstance(actionType);
            SetInputProperties(actionContext, actionType, instance);
            return instance as IAction;
        }
        protected void SetInputProperties(IActionContext actionContext, Type type, object instance)
        {
            var runtimeParams = GetRuntimeInputs(actionContext);
            if (runtimeParams.Count == 0) return;
            var propsMap= type.GetProperties().ToDictionary(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
            foreach (var kv in runtimeParams)
            {
                var prop = propsMap[kv.Key];
                var value = this.convertService.Convert(kv.Value, prop.PropertyType);
                prop.SetValue(instance, value);
            }
        }

        protected Dictionary<string, object> GetRuntimeInputs(IActionContext actionContext)
        {
            var defaultValues = actionContext.MetaInfo.Inputs.Where(p => p.Value.Default != null).ToDictionary(p => p.Key, p => p.Value.Default);

            return defaultValues.Union(actionContext.Parameters.Arguments).ToDictionary(p => p.Key, p => p.Value);
        }
        

    }
}
