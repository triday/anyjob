using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IParameters:IValueProvider
    {
        Dictionary<string, object> ToDictionary();
    }

}
