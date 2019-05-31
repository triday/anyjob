using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob
{
    public static class Extentions
    {
        public static T GetService<T>(this IActionContext context)
        {
            return context.GetService<T>();
        }
        public static T GetRequiredService<T>(this IActionContext context)
        {
            return context.GetRequiredService<T>();
        }
        public static Job Start(this IJobEngine jobService, string actionFullName, Dictionary<string, object> arguments)
        {
            return jobService.Start(actionFullName, arguments, context: new Dictionary<string, object>());
        }

        public static Job Start(this IJobEngine jobService, string actionFullName, Dictionary<string, object> inputs, Dictionary<string, object> context, string executionId = null, int retryCount = 0)
        {
            return jobService.Start(new JobStartInfo()
            {
                ActionFullName = actionFullName,
                ExecutionId = executionId,
                Inputs = inputs,
                Context = context,
                RetryCount = retryCount,
            });
        }
    }
}
