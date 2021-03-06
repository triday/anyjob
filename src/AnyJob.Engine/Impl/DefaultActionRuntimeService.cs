﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using AnyJob.Config;
using Microsoft.Extensions.Options;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultActionRuntimeService : IActionRuntimeService
    {
        public DefaultActionRuntimeService(IOptions<PackOption> packOption)
        {
            this.packOption = packOption;
        }
        private IOptions<PackOption> packOption;
        public IActionRuntime GetRunTime(IActionName actionName)
        {
            _ = actionName ?? throw new ArgumentNullException(nameof(actionName));
            try
            {
                string packDir = Path.Combine(this.packOption.Value.RootDir, actionName.Provider, actionName.Pack, actionName.Version);
                return new ActionRuntime()
                {
                    WorkingDirectory = Path.GetFullPath(packDir),
                    OSPlatForm = GetCurrentOSPlatform()
                };
            }
            catch (Exception ex)
            {
                throw Errors.GetRuntimeInfoError(ex, actionName);
            }

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
