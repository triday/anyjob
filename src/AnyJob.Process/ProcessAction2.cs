namespace AnyJob.Process
{
    public abstract class ProcessAction2 : BaseProcessAction
    {
        public override object Run(IActionContext context)
        {
            var execInputInfo = OnCreateExecInputInfo(context);
            var execOutputInfo = this.StartProcess(context, execInputInfo);
            return this.OnParseResult(context, execInputInfo, execOutputInfo);
        }
        protected abstract ProcessExecInput OnCreateExecInputInfo(IActionContext context);
        protected virtual object OnParseResult(IActionContext context, ProcessExecInput input, ProcessExecOutput output)
        {
            return output.StandardOutput;
        }

    }
}
