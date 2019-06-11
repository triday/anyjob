using AnyJob.Config;
using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionRuntimeService))]
    public class DefaultActionRuntimeService : IActionRuntimeService
    {
        public DefaultActionRuntimeService(PackOption pack)
        {
            this.pack = pack;
        }
        private PackOption pack;
        public IActionRuntime GetRunTime(IActionName actionName)
        {
            string packDir = System.IO.Path.Combine(this.pack.RootDir, actionName.Pack);
            return new ActionRuntime()
            {
                WorkingDirectory = packDir,
            };
        }
    }
}
