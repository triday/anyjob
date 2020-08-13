using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Runner.Internal
{
    public class InternalAction : IAction
    {
        private readonly InternalEntryInfo entryInfo;

        public InternalAction(InternalEntryInfo entryInfo)
        {
            this.entryInfo = entryInfo;
        }
        public object Run(IActionContext context)
        {
            var loadContext = new AssemblyLoadContext("", true);
            try
            {
                var assembly = loadContext.LoadFromAssemblyPath(this.entryInfo.AssemblyFile);
                var type = assembly.GetType(entryInfo.TypeName, true);
                var instance = ActivatorUtilities.CreateInstance(context.ServiceProvider, type) as IAction;
                var result = instance.Run(context);
                var actualResult = GetActualResult(result);
                return SerializeObjectIfNeeded(actualResult, loadContext);
            }
            finally
            {
                if (context != null)
                {
                    loadContext.Unload();
                }
            }
        }
        private object GetActualResult(object result)
        {
            if (result == null) return null;
            var type = result.GetType();
            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(ValueTask<>) || type.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    return (result as dynamic).Result;
                }
            }
            if (type == typeof(Task) || type == typeof(ValueTask))
            {
                (result as dynamic).GetAwaiter().GetResult();
                return null;
            }
            return result;
        }
        private object SerializeObjectIfNeeded(object result, AssemblyLoadContext assemblyLoadContext)
        {
            List<Type> allTypes = new List<Type>();
            var type = result.GetType();
            allTypes.Add(type);
            if (type.IsGenericType)
            {
                allTypes.AddRange(type.GetGenericArguments());
            }
            if (allTypes.Any(p => assemblyLoadContext.Assemblies.Contains(p.Assembly)))
            {
                //需要序列化
            }
            return result;
        }



    }
}
