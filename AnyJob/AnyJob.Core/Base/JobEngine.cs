using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public static class JobEngine
    {
        public static Job Start(JobStartInfo jobStartInfo)
        {
            var jobEngineService = ServiceCenter.GetRequiredService<IJobEngineService>();
            return jobEngineService.Start(jobStartInfo);
        }

        public static Job Start(string actionFullName, Dictionary<string, object> arguments)
        {
            return Start(actionFullName, arguments, context: new Dictionary<string, object>());
        }

        public static Job Start(string actionFullName, Dictionary<string, object> inputs, Dictionary<string, object> context, string executionId = null, int retryCount = 0)
        {
            return Start(new JobStartInfo()
            {
                ActionFullName = actionFullName,
                ExecutionId = executionId,
                Inputs = inputs,
                Context = context,
                RetryCount = retryCount,
            });
        }

        public static bool Cancel(string executionId)
        {
            var jobEngineService = ServiceCenter.GetRequiredService<IJobEngineService>();
            return jobEngineService.Cancel(executionId);
        }
    }
}
