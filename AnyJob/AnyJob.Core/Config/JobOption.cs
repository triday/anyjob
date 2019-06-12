using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Config
{
    [ConfigClass("pack")]
    public class JobOption
    {
        public int MaxJobCount { get; set; }
    }
}
