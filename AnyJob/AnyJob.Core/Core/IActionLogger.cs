using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionLogger
    {
        void WriteLine(string fmt, params object[] args);
    }
}
