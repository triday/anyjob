using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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
            _ = context ?? throw new ArgumentNullException(nameof(context));
            return base.Run(context);
        }
        protected override object OnParseResult(IActionContext context, ProcessExecInput input, ProcessExecOutput output)
        {
            return new AppResult
            {
                ExitCode = output.ExitCode,
                Output = output.StandardOutput
            };
        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            var command = this.AppInfo.Command.Trim();
            var firstIndex = command.IndexOfAny(new[] { ' ', '\t' });
            var (app, args) = firstIndex <= 0 ? (command, string.Empty) : (command.Substring(0, firstIndex), command.Substring(firstIndex + 1));
            var translatedArgs = Translate(args, context.Parameters.Arguments);

            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                StandardInput = string.Empty,
                FileName = app,
                Arguments = new string[] { translatedArgs },
                AppPaths = new List<string>
                {
                   context.RuntimeInfo.WorkingDirectory,
                   Path.Combine(context.RuntimeInfo.WorkingDirectory, AppOption.PackBinPath),
                   Path.GetFullPath(AppOption.GlobalBinPath),
                }
            };
        }
        private string Translate(string item, IDictionary<string, object> input)
        {
            return Regex.Replace(item, "\\$\\{(?<name>\\w+)\\}", (m) =>
            {
                var name = m.Groups["name"].Value;
                if (input.ContainsKey(name))
                {
                    return Convert.ToString(input[name], System.Globalization.CultureInfo.InvariantCulture);
                }
                return m.Value;
            });
        }
    }
}
