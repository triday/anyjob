using System;
using System.Collections.Generic;
using System.IO;
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
            Dictionary<string, string> envs = new Dictionary<string, string>(this.AppInfo.Envs ?? new Dictionary<string, string>());
            envs["PATH"] = GetPathValue(context);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                StandardInput = string.Empty,
                FileName = app,
                Arguments = new string[] { translatedArgs },
                Envs = envs
            };
        }
        private string GetPathValue(IActionContext context)
        {
            string originValue = Environment.GetEnvironmentVariable("PATH");
            return string.Join(System.IO.Path.PathSeparator.ToString(), new[] {
                context.RuntimeInfo.WorkingDirectory,
                Path.GetFullPath(AppOption.PackBinPath),
                Path.GetFullPath(AppOption.GlobalBinPath)
            });
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
