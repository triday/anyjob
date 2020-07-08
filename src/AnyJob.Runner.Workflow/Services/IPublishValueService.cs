namespace AnyJob.Runner.Workflow.Services
{
    public interface IPublishValueService
    {
        void PublishVars(string key, object value, IActionParameter actionParameters);
        void PublishGlobalVars(string key, object value, IActionParameter actionParameters);
    }
}
