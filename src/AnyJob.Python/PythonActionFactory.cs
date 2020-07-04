using System;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AnyJob.Python
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    [YS.Knife.DictionaryKey("python")]
    public class PythonActionFactory : IActionFactoryService
    {
        public PythonActionFactory(IOptions<PythonOption> option)
        {
            this.option = option;
        }
        private IOptions<PythonOption> option;
        public IAction CreateAction(IActionContext actionContext)
        {
            var entryFile = actionContext.MetaInfo.EntryPoint;
            this.AssertEntryFileExits(entryFile, actionContext);
            return new PythonAction(option.Value, entryFile);
        }

        private void AssertEntryFileExits(string entryFile, IActionContext actionContext)
        {
            if (System.IO.Path.IsPathRooted(entryFile))
            {

            }
            string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(actionContext.RuntimeInfo.WorkingDirectory, entryFile));
            if (!System.IO.File.Exists(fullPath))
            {

            }
        }

    }
}
