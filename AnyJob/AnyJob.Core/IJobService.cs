namespace AnyJob
{
    public interface IJobService
    {
        JobInfo Start(StartInfo jobStartInfo);

        bool Cancel(string executionId);

        JobInfo Query(string executionId);
    }
}