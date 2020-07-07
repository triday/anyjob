using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.Runner.Workflow.Actions
{
    public class ForeachAction : SubGroupAction
    {
        public ICollection Source { get; set; }
        public string ItemName { get; set; } = "item";
        public bool Concurrent { get; set; } = false;
        public int StartIndex { get; set; } = 1;
        public bool IgnoreStepError { get; set; } = false;


        protected override object RunInternal(IActionContext context)
        {
            if (Source != null)
            {
                if (Concurrent)
                {
                    RunConcurrent(context, Source);
                }
                else
                {
                    RunOneByOne(context, Source);
                }
            }
            return null;

        }

        protected void RunConcurrent(IActionContext context, ICollection source)
        {
            IIdGenService idGenService = context.GetRequiredService<IIdGenService>();
            IActionExecuterService actionExecuterService = context.GetRequiredService<IActionExecuterService>();
            int index = this.StartIndex;
            var allTasks = new List<Task>();
            foreach (var item in source)
            {
                var task = RunStep(context, index++, item, idGenService, actionExecuterService);
                allTasks.Add(task);
            }
            Task.WaitAll(allTasks.ToArray());

        }
        protected void RunOneByOne(IActionContext context, ICollection source)
        {
            IIdGenService idGenService = context.GetRequiredService<IIdGenService>();
            IActionExecuterService actionExecuterService = context.GetRequiredService<IActionExecuterService>();
            int index = this.StartIndex;
            foreach (var item in source)
            {
                RunStep(context, index++, item, idGenService, actionExecuterService).Wait();
            }
        }

        protected Task RunStep(IActionContext actionContext, int index, object item, IIdGenService idGenService, IActionExecuterService actionExecuterService)
        {
            IExecuteContext executeContext = this.OnCreateExecuteContext(actionContext, idGenService, index, item);
            return actionExecuterService.Execute(executeContext).ContinueWith((result) =>
            {
                if (result.IsCompleted)
                {

                }
            });

        }
        private IExecuteContext OnCreateExecuteContext(IActionContext actionContext, IIdGenService idGenService, int index, object item)
        {
            string newid = idGenService.NewId();
            return new ExecuteContext()
            {
                ActionFullName = "core.group",
                ActionRetryCount = 1,
                ExecuteName = $"{actionContext.ExecuteName}_{index}",
                ExecutePath = actionContext.ExecutePath.NewSubPath(newid),
                Token = actionContext.Token,
            };
        }
    }
}
