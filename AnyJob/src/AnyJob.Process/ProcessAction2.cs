namespace AnyJob.Process
{
    public abstract class ProcessAction2 : IAction
    {
        public virtual object Run(IActionContext context)
        {
            var execInputInfo = OnCreateExecInputInfo(context);
            var execOutputInfo = ProcessExecuter.Exec(execInputInfo);
            this.OnTraceLogger(context, execInputInfo, execOutputInfo);
            this.OnCheckProcessExecOutput(context, execInputInfo, execOutputInfo);
            return this.OnParseResult(context, execInputInfo, execOutputInfo);
        }
        protected abstract ProcessExecInput OnCreateExecInputInfo(IActionContext context);
        protected abstract object OnParseResult(IActionContext context, ProcessExecInput input, ProcessExecOutput output);
        protected virtual void OnTraceLogger(IActionContext context, ProcessExecInput input, ProcessExecOutput output)
        {
            context.Output.WriteLine(output.StandardOutput);
            context.Error.WriteLine(output.StandardError);
        }
        protected virtual void OnCheckProcessExecOutput(IActionContext actionContext, ProcessExecInput input, ProcessExecOutput output)
        {
            if (output.TimeOut)
            {
                throw ErrorFactory.FromCode(nameof(Errors.E80001), input.MaximumTimeSeconds);
            }
            if (output.ExitCode != 0)
            {
                throw ErrorFactory.FromCode(nameof(Errors.E80000), output.ExitCode);
            }
        }
    }
}