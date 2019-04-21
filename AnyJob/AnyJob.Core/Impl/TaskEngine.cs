using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    public class TaskEngine : ServiceContainer, ITaskEngine
    {
        public TaskEngine()
        {
           
        }


        Dictionary<string, ExecuteContext> jobs = new Dictionary<string, ExecuteContext>();


        public Task<ExecuteResult> Execute(string actionRef, ActionParameters actionParameters)
        {
            var resolver = this.GetRequiredService<IActionResolverService>();
            var executer = this.GetRequiredService<IActionExecuterService>();
            var trace = this.GetRequiredService<ITraceService>();
            ExecuteContext context = new ExecuteContext(this);
            trace.Schedule(context);
            var entry = resolver.ResolveRequiredAction(actionRef, actionParameters);

            return executer.Execute(entry, null);
        }

        public TaskResult Start(string actionRef, ActionParameters actionParameters)
        {
            var resolver = this.GetRequiredService<IActionResolverService>();
            var executer = this.GetRequiredService<IActionExecuterService>();

            ExecuteContext context = new ExecuteContext(this);
            throw new NotImplementedException();
        }

        public bool Stop(string executionId)
        {
            throw new NotImplementedException();
        }
    }
}
