using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    public class JobEngine : ServiceContainer, IJobEngine
    {
        public Task<ActionResult> Execute(string actionRef, IActionParameters actionParameters)
        {
            var resolver = this.GetRequiredService<IActionResolver>();
            var executer = this.GetRequiredService<IActionExecuter>();
            var entry = resolver.ResolveAction(actionRef, actionParameters);


            return executer.Execute(entry,null);
        }

       
    }
}
