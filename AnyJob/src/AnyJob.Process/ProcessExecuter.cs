using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace AnyJob.Process
{
    public static class ProcessExecuter
    {
        public static ProcessExecOutput Exec(ProcessExecInput input)
        {
            var args = string.Join(" ", input.Arguments ?? new string[0]);
            ProcessStartInfo startInfo = new ProcessStartInfo(input.FileName, args)
            {
                WorkingDirectory = input.WorkingDir,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = !string.IsNullOrEmpty(input.StandardInput),
                RedirectStandardError = true,
            };
            if (input.Envs != null)
            {
                foreach (var env in input.Envs)
                {
                    startInfo.Environment.Add(env);
                }
            }
            StringBuilder outTextBuilder = new StringBuilder();
            StringBuilder errorTextBuilder = new StringBuilder();
            using (var process = System.Diagnostics.Process.Start(startInfo))
            {
                WriteStdInput(process, input.StandardInput);

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
                    var timeout = input.MaximumTimeSeconds * 1000;
                    if (process.WaitForExit(timeout) && outputWaitHandle.WaitOne(timeout) && errorWaitHandle.WaitOne(timeout))
                    {
                        return new ProcessExecOutput
                        {
                            TimeOut = false,
                            StandardError = errorTextBuilder.ToString(),
                            StandardOutput = outTextBuilder.ToString(),
                            ExitCode = process.ExitCode,
                            ExecutionTime = process.ExitTime - process.StartTime,
                        };
                    }
                    else
                    {

                        return new ProcessExecOutput
                        {
                            TimeOut = true,
                            StandardError = errorTextBuilder.ToString(),
                            StandardOutput = outTextBuilder.ToString(),
                            ExecutionTime = process.ExitTime - process.StartTime,
                        };
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
    }
    public class ProcessExecInput
    {
        public int MaximumTimeSeconds { get; set; } = 600;
        public string FileName { get; set; }
        public string WorkingDir { get; set; }
        public string[] Arguments { get; set; }
        public string StandardInput { get; set; }
        public IDictionary<string, string> Envs { get; set; }
    }

    public class ProcessExecOutput
    {
        public bool TimeOut { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
        public int ExitCode { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}