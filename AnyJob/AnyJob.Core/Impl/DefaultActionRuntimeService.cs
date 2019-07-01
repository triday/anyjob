using AnyJob.Config;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionRuntimeService))]
    public class DefaultActionRuntimeService : IActionRuntimeService
    {
        public DefaultActionRuntimeService(IOptions<PackOption> packOption)
        {
            this.packOption = packOption;
        }
        private IOptions<PackOption> packOption;
        public IActionRuntime GetRunTime(IActionName actionName)
        {
            string packDir = System.IO.Path.Combine(this.packOption.Value.RootDir, actionName.Pack, actionName.Version ?? string.Empty);
            return new ActionRuntime()
            {
                WorkingDirectory = packDir,
            };
        }
    }
}
