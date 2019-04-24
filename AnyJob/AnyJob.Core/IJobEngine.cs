
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IJobEngine: IJobService,IServiceProvider,IDisposable
    {

    }
}
