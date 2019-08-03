using System.Collections.Generic;

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
