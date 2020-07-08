using AnyJob.Runner.App.Model;
using Microsoft.Extensions.Options;

namespace AnyJob.Runner.App
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
            string entryFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(actionContext.RuntimeInfo.WorkingDirectory, entryPoint));
            AppInfo appInfo = fileStoreService.GetObject<AppInfo>(entryFile);
            return new AppAction(appInfo, appOption.Value);
        }
    }
}
