using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Runner.Workflow.Services
{
    public static class Extentions
    {
        public static void PublishGlobalVars(this IPublishValueService publishValueService, IActionParameter actionParameter, IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var kv in values)
            {
                publishValueService.PublishGlobalVars(kv.Key, kv.Value, actionParameter);
            }
        }
        public static void PublishVars(this IPublishValueService publishValueService, IActionParameter actionParameter, IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var kv in values)
            {
                publishValueService.PublishVars(kv.Key, kv.Value, actionParameter);
            }
        }
    }
}
