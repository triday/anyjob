using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Services
{
    public interface IPublishValueService
    {
        void PublishVar(string key, object value, IActionParameter actionParameters);
        void PublishOutput(string key, object value, IActionParameter actionParameters);
    }
}
