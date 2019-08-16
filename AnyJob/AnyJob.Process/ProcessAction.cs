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
            var (fileName, arguments, stdInput) = this.OnGetCommands(context);
            IDictionary<string, string> envs = this.OnGetEnvironment(context);
            string output = this.OnRunProcess(context, workingDir, fileName, arguments, stdInput, envs);

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

        protected abstract (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context);

        protected virtual string OnRunProcess(IActionContext context, string workingDir, string fileName, string arguments, string stdInput, IDictionary<string, string> envs)
        {
            int timeoutSecond = this.OnGetMaximumTimeSeconds(context);
            int timeout = timeoutSecond * 1000;
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments)
            {
                WorkingDirectory = workingDir,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = !string.IsNullOrEmpty(stdInput),
                RedirectStandardError = true,
            };
            foreach (var env in envs)
            {
                startInfo.Environment.Add(env);
            }
            StringBuilder outTextBuilder = new StringBuilder();
            StringBuilder errorTextBuilder = new StringBuilder();
            using (var process = System.Diagnostics.Process.Start(startInfo))
            {
                WriteStdInput(process, stdInput);

                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
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
                    process.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            errorTextBuilder.AppendLine(e.Data);
                        }
                    };
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    if (process.WaitForExit(timeout) && outputWaitHandle.WaitOne(timeout) && errorWaitHandle.WaitOne(timeout))
                    {
                        string output = outTextBuilder.ToString();
                        if (process.ExitCode != 0)
                        {
                            //when process exitcode is not zero, we should collect the output text.
                            context.Logger.WriteLine(output);
                            ErrorFactory.FromCode(nameof(Errors.E80000), process.ExitCode);
                        }
                        return output;
                    }
                    else
                    {
                        //when time out ,we should collect the output text.
                        string output = outTextBuilder.ToString();
                        context.Logger.WriteLine(output);
                        throw ErrorFactory.FromCode(nameof(Errors.E80001), timeoutSecond);
                    }
                }
            }
        }

        private static void WriteStdInput(System.Diagnostics.Process process, string stdInput)
        {
            if (!string.IsNullOrEmpty(stdInput))
            {
                var chars = stdInput.ToCharArray();
                int count = 1024;
                for (int i = 0; i < chars.Length; i += count)
                {
                    int writeCount = Math.Min(count, chars.Length - i);
                    if (writeCount > 0)
                    {
                        process.StandardInput.Write(chars, i, writeCount);
                        process.StandardInput.Flush();
                    }
                }
                process.StandardInput.Close();
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
