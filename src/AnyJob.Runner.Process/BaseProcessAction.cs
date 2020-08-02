using System;
using System.Linq;
namespace AnyJob.Runner.Process
{
    public abstract class BaseProcessAction : IAction
    {
        public abstract object Run(IActionContext context);

        protected ProcessExecOutput StartProcess(IActionContext context, ProcessExecInput execInputInfo)
        {
            var execOutputInfo = ProcessExecuter.Exec(execInputInfo);
            this.OnTraceLogger(context, execInputInfo, execOutputInfo);
            this.OnCheckProcessExecOutput(context, execInputInfo, execOutputInfo);
            return execOutputInfo;
        }
        protected virtual void OnTraceLogger(IActionContext context, ProcessExecInput input, ProcessExecOutput output)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = output ?? throw new ArgumentNullException(nameof(output));
            context.Logger?.WriteLine(output.StandardOutput);
        }
        protected virtual void OnCheckProcessExecOutput(IActionContext context, ProcessExecInput input, ProcessExecOutput output)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = input ?? throw new ArgumentNullException(nameof(input));
            _ = output ?? throw new ArgumentNullException(nameof(output));

            if (output.TimeOut)
            {
                throw ErrorFactory.FromCode(nameof(Errors.E80001), input.MaximumTimeSeconds);
            }
            if (output.ExitCode != 0)
            {
                throw ErrorFactory.FromCode(nameof(Errors.E80000), output.ExitCode);
            }
        }

        protected static string JoinEnvironmentPaths(bool inDocker, params string[] paths)
        {
            if (inDocker)
            {
                return string.Join(":", paths.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.Trim(System.IO.Path.PathSeparator).ToUnixPath()));
            }
            return string.Join(System.IO.Path.PathSeparator.ToString(), paths.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.Trim(System.IO.Path.PathSeparator)));
        }
    }
}
