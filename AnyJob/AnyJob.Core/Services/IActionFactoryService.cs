using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionFactoryService
    {
        ActionEntry Get(string actionRef);
    }
}
