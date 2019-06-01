using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public interface IActionInputDefination
    {
        string Name { get;  }

        string Description { get; }

        object DefaultValue { get;  }

        bool IsRequired { get;  }

        bool IsSecret { get;  }

        IActionType Type { get;  }

    }
}
