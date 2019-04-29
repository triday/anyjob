using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public interface IActionOutputMeta
    {
        string Type { get;  }

        string Description { get;  }

        bool IsRequired { get;  }
    }
}
