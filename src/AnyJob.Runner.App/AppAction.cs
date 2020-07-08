using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AnyJob.Runner.App.Model;
using AnyJob.Runner.Process;

namespace AnyJob.Runner.App
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

        public override object Run(IActionContext context)
        {
            this.CheckPlatforms(this.AppInfo, context);
            return base.Run(context);
        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
        {

            var command = this.AppInfo.Command.Trim();
            var firstIndex = command.IndexOfAny(new[] { ' ', '\t' });
            var (app, args) = firstIndex <= 0 ? (command, string.Empty) : (command.Substring(0, firstIndex), command.Substring(firstIndex + 1));
            var fileName = this.FindAppFullPath(context, app);
            var translatedArgs = Translate(args, context.Parameters.Arguments);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                StandardInput = string.Empty,
                FileName = fileName,
                Arguments = new string[] { translatedArgs },
                Envs = this.AppInfo.Envs ?? new Dictionary<string, string>()
            };
        }


        private string Translate(string item, IDictionary<string, object> input)
        {
            return Regex.Replace(item, "\\$\\{(?<name>\\w+)\\}", (m) =>
            {
                var name = m.Groups["name"].Value;
                if (input.ContainsKey(name))
                {
                    return Convert.ToString(input[name]);
                }
                return m.Value;
            });
        }
        private void CheckPlatforms(AppInfo appInfo, IActionContext context)
        {
            var currentPlatform = context.RuntimeInfo.OSPlatForm.ToString();
            var supportPlatforms = (appInfo.SupportPlatforms ?? Array.Empty<string>());
            if (!supportPlatforms.Contains(currentPlatform, StringComparer.InvariantCultureIgnoreCase))
            {
                throw new ActionException($"The app action '{context.Name}' not support platform '{currentPlatform}.'");
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
