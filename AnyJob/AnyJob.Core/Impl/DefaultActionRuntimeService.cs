using AnyJob.Config;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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
            string packDir = Path.Combine(this.packOption.Value.RootDir, actionName.Pack, actionName.Version ?? string.Empty);
            return new ActionRuntime()
            {
                WorkingDirectory = Path.GetFullPath(packDir),
                OSPlatForm = GetCurrentOSPlatform()
            };
        }
        private OSPlatform GetCurrentOSPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }
            else
            {
                return OSPlatform.OSX;
            }
        }
    }
}
