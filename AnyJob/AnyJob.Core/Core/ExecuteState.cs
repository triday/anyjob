using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public enum ExecuteState
    {
        Requested=0,
        Ready=1,
        Running=2,
        Success=3,
        Failure=4,
    }
}
