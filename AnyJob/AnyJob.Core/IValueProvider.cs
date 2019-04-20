using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IValueProvider
    {
        object GetValue(string key);
    }
}
