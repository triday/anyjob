using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Services
{
    public interface IPublishValueService
    {
        void PublishVars(string key, object value, IActionParameter actionParameters);
        void PublishGlobalVars(string key, object value, IActionParameter actionParameters);
    }
}
