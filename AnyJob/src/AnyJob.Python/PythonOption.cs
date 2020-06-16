using AnyJob.DependencyInjection;

namespace AnyJob.Python
{
    [YS.Knife.OptionsClass("python")]
    public class PythonOption
    {
        public string DockerImage {get;set;} = "python:3.7";
        public string PythonPath { get; set; } = "python";
        public string WrapperPath { get; set; } = "global/python/python_wrapper.py";
        public string GlobalPythonLibPath { get; set; } = "global/python/python_libs";
        public string PackPythonLibPath { get; set; } = "python_libs";
    }
}
