namespace AnyJob.Runner.Workflow.Config
{
    [YS.Knife.OptionsClass("workflow")]
    public class WorkflowOption
    {
        public string[] SubEntryActions { get; set; } = new string[] { "workflow.group", "workflow.loop", "workflow.for" };
        public string SubEntryActionVarName { get; set; } = "__subentry";
    }
}
