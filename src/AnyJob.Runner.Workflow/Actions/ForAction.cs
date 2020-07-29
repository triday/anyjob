using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyJob.Runner.Workflow.Actions
{
    public class ForAction : SubGroupAction
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
            foreach (var val in values ?? Enumerable.Empty<int>())
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

        protected async Task RunStep(IActionContext actionContext, int value, IIdGenService idGenService, IActionExecuterService actionExecuterService)
        {
            _ = actionExecuterService ?? throw new ArgumentNullException(nameof(actionExecuterService));
            _ = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
            _ = idGenService ?? throw new ArgumentNullException(nameof(idGenService));
            IExecuteContext executeContext = this.OnCreateExecuteContext(actionContext, idGenService, value);
            var result = await actionExecuterService.Execute(executeContext);



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
