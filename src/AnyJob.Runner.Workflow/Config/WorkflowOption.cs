namespace AnyJob.Runner.Workflow.Config
{
    [YS.Knife.OptionsClass("workflow")]
    public class WorkflowOption
    {
        public string[] SubEntryActions { get; set; } = new string[] { "core.group", "core.loop", "core.for" };
        public string SubEntryActionVarName { get; set; } = "__subentry";
    }
}
