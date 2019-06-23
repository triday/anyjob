using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionInputDefination
    {
        string Description { get; }

        object Default { get;  }

        bool IsRequired { get;  }

        bool IsSecret { get;  }

        IActionType Type { get;  }

    }
}
