using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionOutputDefination
    {
        IActionType Type { get;  }

        string Description { get;  }

        bool IsSecret { get;  }

        bool IsRequired { get; }
    }
}
