using AnyJob.Workflow.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AnyJob.Workflow.Actions
{
    public class For : SubGroupAction
    {
        public string ItemName { get; set; } = "i";
        public int StartValue { get; set; } = 0;
        public int Count { get; set; }
        public int StepValue { get; set; } = 1;
        public bool Concurrent { get; set; } = false;
        public bool IgnoreStepError { get; set; } = false;
        protected override object RunInternal(IActionContext context)
        {
            int count = Math.Max(0, this.Count);
            var values = Enumerable.Range(0, count).Select(p => p * StepValue + StartValue);
            if (Concurrent)
            {
                RunConcurrent(context, values);
            }
            else
            {
                RunOneByOne(context, values);
            }
            return null;
        }
        protected virtual void RunOneByOne(IActionContext context, IEnumerable<int> values)
        {
            IIdGenService idGenService = context.GetRequiredService<IIdGenService>();
            IActionExecuterService actionExecuterService = context.GetRequiredService<IActionExecuterService>();
            foreach (var val in values)
            {
                RunStep(context, val, idGenService, actionExecuterService).Wait();
            }
        }

        protected virtual void RunConcurrent(IActionContext context, IEnumerable<int> values)
        {
            IIdGenService idGenService = context.GetRequiredService<IIdGenService>();
            IActionExecuterService actionExecuterService = context.GetRequiredService<IActionExecuterService>();
            var allTasks = values.Select(p => RunStep(context, p, idGenService, actionExecuterService)).ToArray();
            Task.WaitAll(allTasks);
        }

        protected Task RunStep(IActionContext actionContext, int value, IIdGenService idGenService, IActionExecuterService actionExecuterService)
        {
            IExecuteContext executeContext = this.OnCreateExecuteContext(actionContext, idGenService, value);
            return actionExecuterService.Execute(executeContext).ContinueWith((result) =>
            {
                if (result.IsCompleted)
                {

                }
            });

        }
        private IExecuteContext OnCreateExecuteContext(IActionContext actionContext, IIdGenService idGenService, int value)
        {
            string newid = idGenService.NewId();
            return new ExecuteContext()
            {
                ActionFullName = "core.group",
                ActionRetryCount = 1,
                ExecuteName = $"{actionContext.ExecuteName}_{value}",
                ExecutePath = actionContext.ExecutePath.NewSubPath(newid),
                Token = actionContext.Token,
            };
        }

    }
}
