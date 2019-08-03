using AnyJob.App.Model;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.App
{
    [ServiceImplClass(Key ="app")]
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
