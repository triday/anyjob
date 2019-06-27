using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

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
            
            return OnParseResult(context, output);
        }

        protected virtual object OnParseResult(IActionContext context, string output)
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
            int timeout = timeoutSecond * 1000;
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
            StringBuilder outTextBuilder = new StringBuilder();
            using (var process = System.Diagnostics.Process.Start(startInfo))
            {
                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            outTextBuilder.AppendLine(e.Data);
                        }
                    };
                    process.BeginOutputReadLine();
                    if (process.WaitForExit(timeout) && outputWaitHandle.WaitOne(timeout))
                    {
                        CheckExitCode(context, process.ExitCode);
                        return outTextBuilder.ToString().TrimEnd();
                    }
                    else
                    {
                        throw ErrorFactory.FromCode(nameof(Errors.E80001), timeoutSecond);
                    }
                }
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
