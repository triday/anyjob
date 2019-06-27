using AnyJob.App.Model;
using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.App
{
    [ServiceImplClass(typeof(IActionFactoryService))]
    public class AppActionFactory : IActionFactoryService
    {
        private IFileStoreService fileStoreService;
        public AppActionFactory(IFileStoreService fileStoreService)
        {
            this.fileStoreService = fileStoreService;
        }
        public string ActionKind => "app";

        public IAction CreateAction(IActionContext actionContext)
        {
            string entryPoint = actionContext.MetaInfo.EntryPoint;
            string entryFile = System.IO.Path.GetFullPath(entryPoint, actionContext.RuntimeInfo.WorkingDirectory);
            AppInfo appInfo = fileStoreService.ReadObject<AppInfo>(entryFile);
            return new AppAction(appInfo);
        }
    }
}
