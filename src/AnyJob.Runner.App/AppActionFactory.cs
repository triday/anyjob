using System;
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
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            AppInfo appInfo = ParseAppEntryInfo(actionContext.MetaInfo.EntryPoint);
            return new AppAction(appInfo, appOption.Value);
        }
        private AppInfo ParseAppEntryInfo(string entry)
        {
            return new AppInfo
            {
                Command = entry
            };
        }
    }
}
