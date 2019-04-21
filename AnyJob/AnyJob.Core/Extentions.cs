using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AnyJob
{
    public static class Extentions
    {

        public static T GetRequiredService<T>(this IServiceProvider provider)
        {
            var serviceInstance = provider.GetService<T>();
            if (serviceInstance == null)
            {
                throw new ActionException($"The service \"{typeof(T).AssemblyQualifiedName}\" is required, but not found.");
            }
            else
            {
                return serviceInstance;
            }
        }
        public static void RunRequiredService<T>(this IServiceProvider provider, Action<T> action)
        {
            var serviceInstance = GetRequiredService<T>(provider);
            action?.Invoke(serviceInstance);
        }

        public static IEnumerable<T> GetMultiServices<T>(this IServiceProvider provider)
        {
            return provider.GetInstance<IEnumerable<T>>() ?? Enumerable.Empty<T>();
        }
        public static void RegisteMultiService<T>(this IServiceContainer container, params T[] items)
        {
            container.RegisteType<IEnumerable<T>>(items ?? new T[0]);
        }

        public static ActionEntry ResolveRequiredAction(this IActionResolverService resolver,string actionRef, ActionParameters parameters)
        {
            var actionEntry = resolver.ResolveAction(actionRef, parameters);
            if (actionEntry == null)
            {
                throw new ActionException($"Can not find action \"{actionRef}\".");
            }
            else
            {
                return actionEntry;
            }
           
        }
    }
}
