using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AnyJob.Process
{
    public abstract class ProcessAction : IAction
    {

        public virtual int OnGetMaximumTimeSeconds(IActionContext context)
        {
            return 60 * 10;
        }

        public virtual object Run(IActionContext context)
        {
            string workingDir = context.RuntimeInfo.WorkingDirectory;
            var (fileName, arguments) = this.OnGetCommands(context);
            IDictionary<string, string> envs = this.OnGetEnvironment(context);
            string output = this.OnRunProcess(context, workingDir, fileName, arguments, envs);
            return OnParseResult(output);
        }

        protected virtual object OnParseResult(string output)
        {
            return output;
        }

        protected virtual IDictionary<string, string> OnGetEnvironment(IActionContext context)
        {
            return new Dictionary<string, string>();
        }

        protected abstract (string FileName, string Arguments) OnGetCommands(IActionContext context);

        protected virtual string OnRunProcess(IActionContext context, string workingDir, string fileName, string arguments, IDictionary<string, string> envs)
        {
            int timeoutSecond = this.OnGetMaximumTimeSeconds(context);
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments)
            {
                WorkingDirectory = workingDir,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            foreach (var env in envs)
            {
                startInfo.Environment.Add(env);
            }
            var process = System.Diagnostics.Process.Start(startInfo);
            StringBuilder outTextBuilder = new StringBuilder();
            process.OutputDataReceived += (s, e) =>
            {
                outTextBuilder.AppendLine(e.Data);
            };
            process.BeginOutputReadLine();

            if (process.WaitForExit(timeoutSecond * 1000))
            {
                CheckExitCode(context, process.ExitCode);
                return outTextBuilder.ToString().TrimEnd();
            }
            else
            {
                throw ErrorFactory.FromCode(nameof(Errors.E80001), timeoutSecond);
            }
        }

        protected virtual void CheckExitCode(IActionContext actionContext, int code)
        {
            if (code != 0)
            {
                throw ErrorFactory.FromCode(nameof(Errors.E80000), code);
            }
        }
    }
}
