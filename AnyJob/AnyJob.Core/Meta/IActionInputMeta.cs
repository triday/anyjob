using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public interface IActionInputMeta
    {
        string Name { get;  }

        string Description { get; }

        object DefaultValue { get;  }

        bool IsRequired { get;  }

        bool IsSecret { get;  }

        string Type { get;  }

    }
}
