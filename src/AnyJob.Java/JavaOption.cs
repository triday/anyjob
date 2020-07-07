using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.DependencyInjection;

namespace AnyJob.Runner.Java
{
    [YS.Knife.OptionsClass("java")]
    public class JavaOption
    {
        public string JavaPath { get; set; } = "java";
        public string WrapperPath { get; set; } = "global/java/java_wrapper.jar";
        public string GlobalJarsPath { get; set; } = "global/java/jar_libs";
        public string PackJarsPath { get; set; } = "jar_libs";
    }
}
