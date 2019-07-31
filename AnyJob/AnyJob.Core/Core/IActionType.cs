using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionType
    {
        object Default { get; }

        bool IsMust { get; }

        bool Validate(object value, out IList<string> errorMessages);

        object Protect(object value);
    }
}
