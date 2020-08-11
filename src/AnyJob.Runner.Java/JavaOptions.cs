namespace AnyJob.Runner.Java
{
    [YS.Knife.OptionsClass("java")]
    public class JavaOptions
    {
        public string JavaPath { get; set; } = "java";
        public string WrapperPath { get; set; } = "global/java/jar_libs/java_warpper-1.0.jar";
    }
}
