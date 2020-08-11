using System.Linq;
using AnyJob;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife;

namespace AnyJob.Runner.Java
{
    [ServiceClass(Lifetime = ServiceLifetime.Singleton)]
    [DictionaryKey("java")]
    public class JavaActionFactory : IActionFactoryService
    {
        private readonly JavaOptions javaOptions;

        public JavaActionFactory(JavaOptions javaOptions)
        {
            this.javaOptions = javaOptions;
        }
        public IAction CreateAction(IActionContext actionContext)
        {
            var entryInfo = ParseEntryInfo(actionContext.MetaInfo.EntryPoint);
            return new JavaAction(javaOptions, entryInfo);
        }

        private JavaEntryInfo ParseEntryInfo(string entry)
        {
            var items = entry.Split(',').Select(p => p.Trim()).ToArray();
            if (items.Length != 2)
            {
                throw new System.Exception("invalid entry");
            }
            return new JavaEntryInfo
            {
                ClassFullName = items[0],
                MethodName = items[1]
            };
        }
    }
}
