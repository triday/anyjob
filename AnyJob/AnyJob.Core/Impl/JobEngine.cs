using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnyJob.DI;
namespace AnyJob.Impl
{
    public class JobEngine : DIContainer, IJobEngine
    {
        Dictionary<string, Job> jobs = new Dictionary<string, Job>();

        public bool Cancel(string jobId)
        {
            if (this.jobs.TryGetValue(jobId, out var job))
            {
                job.CancelTokenSource.Cancel();

                return true;
            }
            else
            {
                return false;
            }
        }

        public Job Start(string actionRef, ActionParameters actionParameters)
        {
            return Start(actionRef, actionParameters, Guid.NewGuid().ToString());
        }

        public Job Start(string actionRef, ActionParameters actionParameters, string executionId)
        {
            if (string.IsNullOrEmpty(executionId))
            {
                throw new ArgumentNullException(nameof(executionId));
            }
            if (jobs.ContainsKey(executionId))
            {
                throw new ActionException($"The execution id \"${executionId}\" is already in the task engine.");
            }
            var resolver = this.GetRequiredService<IActionResolverService>();
            var executer = this.GetRequiredService<IActionExecuterService>();

            ExecuteContext context = new ExecuteContext(this);

            var task = executer.Execute(context);
            throw new NotImplementedException();
        }

        public Job Start(JobStartInfo jobStartInfo)
        {
            var resolver = this.GetRequiredService<IActionResolverService>();
            var executer = this.GetRequiredService<IActionExecuterService>();

            var actionEntry = resolver.ResolveAction(jobStartInfo.ActionRef, new ActionParameters(jobStartInfo.Inputs, jobStartInfo.Context));


            var job = new Job()
            {
                JobId = jobStartInfo.JobId,
                 ActionEntry =actionEntry,
                 CancelTokenSource =jobStartInfo.CancelTokenSource,
                 // Task=executer.Execute()
            };

            return this.jobs[job.JobId] = job;
        }




    }
}
