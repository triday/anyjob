using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Config
{
    [ConfigClass("pack")]
    public class PackOption
    {
        public string RootDir { get; set; } = "packs";

    }
}
