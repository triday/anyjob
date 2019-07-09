using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Services
{
    public interface IDynamicValueService
    {
        object GetDynamicValue(object value, IActionParameter actionParameter);
    }
}
