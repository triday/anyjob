namespace AnyJob.Runner.Workflow.Config
{
    public class WorkflowOption
    {
        public string[] SubEntryActions = new string[] { "core.group", "core.loop", "core.for" };
        public string SubEntryActionVarName = "__subentry";
    }
}
