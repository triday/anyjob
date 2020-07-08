namespace AnyJob.Runner.App
{
    [YS.Knife.OptionsClass("node")]
    public class AppOption
    {
        public string GlobalBinPath { get; set; } = "global/app/bin";
        public string PackBinPath { get; set; } = "bin";
    }
}
