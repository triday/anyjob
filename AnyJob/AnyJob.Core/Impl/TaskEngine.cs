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


       

        public TaskResult Start(string actionRef, ActionParameters actionParameters)
        {
            var resolver = this.GetRequiredService<IActionResolverService>();
            var executer = this.GetRequiredService<IActionExecuterService>();

            ExecuteContext context = new ExecuteContext(this);

            var task = executer.Execute(context);
            throw new NotImplementedException();
        }

        public bool Stop(string executionId)
        {
            throw new NotImplementedException();
        }
    }
}
