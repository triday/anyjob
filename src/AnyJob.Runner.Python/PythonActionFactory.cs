using System;
using System.Linq;
using Microsoft.Extensions.Options;

namespace AnyJob.Runner.Python
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
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            //var entryFile = actionContext.MetaInfo.EntryPoint;
            var entryInfo = ParseEntryInfo(actionContext.MetaInfo.EntryPoint);
            // this.AssertEntryFileExits(entryFile, actionContext);
            return new PythonAction(option.Value, entryInfo);
        }
        private PythonEntryInfo ParseEntryInfo(string entry)
        {
            var items = entry.Split(',').Select(p => p.Trim()).ToArray();
            if (items.Length == 2)
            {
                return new PythonEntryInfo
                {
                    Module = GetEntryModuleName(items[0]),
                    Method = items[1]
                };
            }
            else
            {
                throw new Exception("Invalid entry format.");
            }
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
        private string GetEntryModuleName(string entryModule)
        {
            //.py or .pyc
            string nameWithOutExt = System.IO.Path.GetFileNameWithoutExtension(entryModule);

            return nameWithOutExt.Replace('/', '.')
                    .Replace('\\', '.')
                    .Trim('.');
        }

    }
}
