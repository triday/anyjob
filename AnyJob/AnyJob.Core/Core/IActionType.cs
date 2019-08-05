using System.Collections.Generic;

namespace AnyJob
{

    public interface IActionType
    {
        object DefaultValue { get; }

        bool IsMust { get; }

        bool Validate(object value, out IList<string> errorMessages);

        object MaskSecret(object value);
    }
}
