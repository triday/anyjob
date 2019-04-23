using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

        public Job Start(JobStartInfo jobStartInfo)
        {
            if (jobStartInfo == null)
            {
                throw new ArgumentNullException(nameof(jobStartInfo));
            }

            var resolver = this.GetRequiredService<IActionResolverService>();
            var executer = this.GetRequiredService<IActionExecuterService>();
            var idGen = this.GetRequiredService<IIdGenService>();
            var executionId = String.IsNullOrEmpty(jobStartInfo.ExecutionId) ? idGen.NewId() : jobStartInfo.ExecutionId;
            var parameters = jobStartInfo.Parameters ?? new ActionParameters();
            var actionEntry = resolver.ResolveAction(jobStartInfo.ActionRef);
            var cancelSource = jobStartInfo.TimeoutSeconds > 0 ? new CancellationTokenSource(jobStartInfo.TimeoutSeconds * 1000) : new CancellationTokenSource();
            var executerContext = new ExecuteContext(this)
            {
                ExecutionId = executionId,
                ActionRef = jobStartInfo.ActionRef,
                ParentExecutionId = null,
                RootExecutionId = executionId,
                ActionParameters = parameters,
                CancelTokenSource = cancelSource,
            };

            var task = executer.Execute(executerContext);
            var job = new Job()
            {
                ExecutionId = executionId,
                ActionEntry = actionEntry,
                CancelTokenSource = new CancellationTokenSource(),
                ActionParameters = parameters,
                Task = task
            };
            return this.jobs[executionId] = job;
        }




    }
}
