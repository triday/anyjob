using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Loader
{
    public interface IAnyjobAssembly
    {
        void Registe(IServiceCollection services);
    }
}
