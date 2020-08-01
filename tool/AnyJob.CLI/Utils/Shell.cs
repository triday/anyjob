using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AnyJob.CLI.Utils
{
    public static class Shell
    {
        public static int Exec(string fileName, string arguments, bool throwIfExitCodeNotZero = false)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,

            });
            process.OutputDataReceived += (s, _e) => Console.WriteLine(_e.Data);
            process.ErrorDataReceived += (s, _e) => Console.WriteLine(_e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            if (process.ExitCode != 0 && throwIfExitCodeNotZero)
            {
                throw new Exception($"Exec process return {process.ExitCode}.");
            }
            return process.ExitCode;
        }
    }
}
