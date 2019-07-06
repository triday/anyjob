using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Services
{
    public interface IPublishValueService
    {
        void PublishVar(string key, string value, IActionParameters actionParameters);
        void PublishOutput(string key, string value, IActionParameters actionParameters);
    }
}
