using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public interface IActionMeta
    {

        string Kind { get;  }

        string Description { get;  }

        bool Enabled { get; }

        IEnumerable<IActionInputMeta> Inputs { get; }

        IActionOutputMeta Output { get; }


    }
}
