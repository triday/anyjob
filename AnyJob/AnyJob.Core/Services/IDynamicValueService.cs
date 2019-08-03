using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示动态值的服务
    /// </summary>
    public interface IDynamicValueService
    {
        object GetDynamicValue(object value, IActionParameter actionParameter);
    }
}
