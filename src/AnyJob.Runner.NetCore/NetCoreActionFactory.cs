﻿using System;
using System.Linq;
using System.Reflection;
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
            var (assemblyFile, typeName) = SplitAssemblyName(actionContext);
            var assembly = Assembly.LoadFrom(assemblyFile);

            var actionType = assembly.GetType(typeName);
            var instance = Activator.CreateInstance(actionType);
            SetInputProperties(actionContext, actionType, instance);
            return instance as IAction;
        }
        private (string AssemblyFile, string TypeName) SplitAssemblyName(IActionContext actionContext)
        {
            string entryPoint = actionContext.MetaInfo.EntryPoint;
            var index = entryPoint.LastIndexOf(',');
            if (index <= 0)
            {
                throw new Exception("Incorrect entry point format.");
            }
            var assemblyFile = entryPoint.Substring(index + 1);
            string assemblyFullFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(actionContext.RuntimeInfo.WorkingDirectory, assemblyFile));
            var typeName = entryPoint.Substring(0, index).Trim();
            return (assemblyFullFile, typeName);
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
