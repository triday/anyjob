using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ITimeService
    {
        DateTimeOffset Now();
    }
}
