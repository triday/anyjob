namespace AnyJob.Runner.Java
{
    [YS.Knife.OptionsClass("java")]
    public class JavaOptions
    {
        public string JavaPath { get; set; } = "java";
        public string GlobalJarLibsPath { get; set; } = "global/java/jar_libs";
        public string EntryClass { get; set; } = "entry.Main";
    }
}
