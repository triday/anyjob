using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.App
{
    [ConfigClass("node")]
    public class AppOption
    {
        public string GlobalBinPath { get; set; } = "global/app/bin";
        public string PackBinPath { get; set; } = "bin";
    }
}
