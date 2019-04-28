using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public interface IActionMeta
    {
        string Ref { get;  }

        string Kind { get;  }

        string Description { get;  }

        string DisplayFormat { get; }

        string EntryPoint { get; }



    }
}
