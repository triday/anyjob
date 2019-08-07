using AnyJob.DependencyInjection;

namespace AnyJob.Python
{
    [ConfigClass("python")]
    public class PythonOption
    {
        public string PythonPath { get; set; } = "python";
        public string WrapperPath { get; set; } = "global/python/python_wrapper.py";
        public string GlobalPythonLibPath { get; set; } = "global/python/python_libs";
        public string PackPythonLibPath { get; set; } = "python_libs";
    }
}
