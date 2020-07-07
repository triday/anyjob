using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
namespace AnyJob.Runner.Process
{
    public static class ProcessExecuter
    {

        public static ProcessExecInput BuildDockerProcess(string imageName, string[] args, string workingDir = null, IDictionary<string, string> volumeMaps = null, IDictionary<string, string> envs = null, string standardInput = null)
        {
            var arguments = new List<string>()
            {
                "run",
                "--rm",
            };
            if (!string.IsNullOrEmpty(workingDir))
            {
                arguments.Add("-w");
                arguments.Add(workingDir);
            }
            if (volumeMaps != null)
            {
                foreach (var kv in volumeMaps)
                {
                    arguments.Add("-v");
                    arguments.Add($"{kv.Key}:{kv.Value}");
                }
            }
            if (envs != null)
            {
                foreach (var kv in envs)
                {
                    arguments.Add("-e");
                    arguments.Add($"{kv.Key}=\"{kv.Value}\"");
                }
            }
            arguments.Add(imageName);
            arguments.AddRange(args ?? Enumerable.Empty<string>());
            return new ProcessExecInput
            {
                FileName = "docker",
                StandardInput = standardInput,
                WorkingDir = string.Empty,
                Arguments = arguments.ToArray(),
                Envs = new System.Collections.Generic.Dictionary<string, string>()
            };
        }
        public static ProcessExecOutput Exec(ProcessExecInput input)
        {
            var args = string.Join(" ", input.Arguments ?? Array.Empty<string>());
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
                        };
                    }
                    else
                    {

                        return new ProcessExecOutput
                        {
                            TimeOut = true,
                            StandardError = errorTextBuilder.ToString(),
                            StandardOutput = outTextBuilder.ToString(),
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
    }
}
