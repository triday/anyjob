using AnyJob.App.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnyJob.Process;
using System.Runtime.InteropServices;

namespace AnyJob.App
{
    public class AppAction : ProcessAction2
    {
        public AppAction(AppInfo appInfo, AppOption appOption)
        {
            this.AppInfo = appInfo;
            this.AppOption = appOption;
        }
        public AppOption AppOption { get; private set; }
        public AppInfo AppInfo { get; private set; }
        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
        {
            var items = this.AppInfo.Command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var args = context.Parameters.Arguments;
            var translateItems = items.Select(p => Translate(p, args));
            var fileName = this.FindAppFullPath(context, translateItems.First());
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                StandardInput = string.Empty,
                FileName = fileName,
                Arguments = translateItems.Skip(1).ToArray(),
                Envs = this.AppInfo.Envs ?? new Dictionary<string, string>()
            };
        }


        private string Translate(string item, IDictionary<string, object> input)
        {
            if (item.StartsWith("${") && item.EndsWith("}"))
            {
                string key = item.Substring(2, item.Length - 3);
                return Convert.ToString(input[key]);
            }
            else
            {
                return item;
            }
        }


        private string FindAppFullPath(IActionContext context, string appName)
        {
            if (System.IO.Path.IsPathRooted(appName))
            {
                //绝对路径
                return appName;
            }
            string[] searchDirs = new string[] {
                context.RuntimeInfo.WorkingDirectory,
                System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory,AppOption.PackBinPath),
                System.IO.Path.GetFullPath(AppOption.GlobalBinPath)
            };
            if (context.RuntimeInfo.OSPlatForm == OSPlatform.Windows)
            {
                return FindWindowsAppFullName(searchDirs, appName);
            }
            else
            {
                return FindOtherOSAppFullName(searchDirs, appName);
            }


        }
        private string FindWindowsAppFullName(string[] searchDirs, string appName)
        {
            if (System.IO.Path.HasExtension(appName))
            {
                if (SearchAppFullName(searchDirs, appName, out string appFullName))
                {
                    return appFullName;
                }
                else
                {
                    return appName;
                }
            }
            else
            {
                foreach (var ext in new string[] { ".com", ".exe", ".bat" })
                {
                    if (SearchAppFullName(searchDirs, System.IO.Path.ChangeExtension(appName, ext), out string appFullName))
                    {
                        return appFullName;
                    }
                }
                return appName;
            }
        }

        private bool SearchAppFullName(string[] searchDirs, string appName, out string appFullName)
        {
            foreach (var baseDir in searchDirs)
            {
                string fullName = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, appName));
                if (System.IO.File.Exists(fullName))
                {
                    appFullName = fullName;
                    return true;
                }
            }
            appFullName = appName;
            return false;
        }

        private string FindOtherOSAppFullName(string[] searchDirs, string appName)
        {
            if (SearchAppFullName(searchDirs, appName, out string appFullName))
            {
                return appFullName;
            }
            else
            {
                return appName;
            }
        }
    }
}
