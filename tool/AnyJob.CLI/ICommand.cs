using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.CLI
{
    public interface ICommand
    {
        int Run();
    }
}
