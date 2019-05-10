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
        public static Job Start(this IJobEngine jobService, string actionRef, Dictionary<string, object> arguments)
        {
            return jobService.Start(actionRef, arguments, context: new Dictionary<string, object>());
        }

        public static Job Start(this IJobEngine jobService, string actionRef, Dictionary<string, object> inputs, Dictionary<string, object> context, string executionId = null, int retryCount = 0)
        {
            return jobService.Start(new JobStartInfo()
            {
                ActionRef = actionRef,
                ExecutionId = executionId,
                Inputs = inputs,
                Context = context,
                RetryCount = retryCount,
            });
        }
    }
}
