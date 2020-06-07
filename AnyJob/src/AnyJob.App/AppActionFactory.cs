using AnyJob.App.Model;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AnyJob.App
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    [YS.Knife.DictionaryKey("app")]
    public class AppActionFactory : IActionFactoryService
    {
        private IObjectStoreService fileStoreService;
        private IOptions<AppOption> appOption;
        public AppActionFactory(IObjectStoreService fileStoreService, IOptions<AppOption> options)
        {
            this.appOption = options;
            this.fileStoreService = fileStoreService;

        }

        public IAction CreateAction(IActionContext actionContext)
        {
            string entryPoint = actionContext.MetaInfo.EntryPoint;
            string entryFile = System.IO.Path.GetFullPath(entryPoint, actionContext.RuntimeInfo.WorkingDirectory);
            AppInfo appInfo = fileStoreService.GetObject<AppInfo>(entryFile);
            return new AppAction(appInfo, appOption.Value);
        }
    }
}
