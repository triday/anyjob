using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IDynamicValueService
    {
        object GetDynamicValue(object value, IActionParameter actionParameter);
    }
}
