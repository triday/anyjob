using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public interface IActionType
    {
        Type GetRunTimeType();
    }
}
