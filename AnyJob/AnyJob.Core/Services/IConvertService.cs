using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IConvertService
    {
        object ConvertTo(object value, Type targetType);
    }
}
