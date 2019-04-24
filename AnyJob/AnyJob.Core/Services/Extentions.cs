﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public static partial class Extentions
    {

        public static Job Start(this IJobService jobService, string actionRef, Dictionary<string, object> arguments)
        {
            return jobService.Start(actionRef, arguments, context: new Dictionary<string, object>());
        }

        public static Job Start(this IJobService jobService, string actionRef, Dictionary<string, object> inputs, Dictionary<string, object> context, string executionId = null, int timeoutSeconds = 0)
        {
            return jobService.Start(new JobStartInfo()
            {
                ActionRef = actionRef,
                ExecutionId = executionId,
                Inputs = inputs,
                Context = context,
                TimeoutSeconds = timeoutSeconds,
            });
        }
    }
}
