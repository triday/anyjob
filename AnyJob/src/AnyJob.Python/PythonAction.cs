using AnyJob.Process;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnyJob.Python
{
    public class PythonAction : TypedProcessAction
    {
        public PythonAction(PythonOption pythonOption, string entryFile)
        {
            this.pythonOption = pythonOption;
            this.entryFile = entryFile;
        }
        private PythonOption pythonOption;
        private string entryFile;

        protected override (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context)
        {
            ISerializeService serializeService = context.GetRequiredService<ISerializeService>();
            string wrapperPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, pythonOption.WrapperPath));
            string paramsText = serializeService.Serialize(context.Parameters.Arguments);
            string entryModule = this.GetEntryModuleName(this.entryFile);
            string[] args = new string[] { wrapperPath, entryModule };
            return (pythonOption.PythonPath, string.Join(" ", args), paramsText);
        }
        protected override IDictionary<string, string> OnGetEnvironment(IActionContext context)
        {
            var currentEnv = base.OnGetEnvironment(context);
            string currentNodeModulesPath = System.Environment.GetEnvironmentVariable("PYTHONPATH");
            string packNodeModulesPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory, pythonOption.PackPythonLibPath));
            string globalNodeModulesPath = System.IO.Path.GetFullPath(pythonOption.GlobalPythonLibPath);
            string workingDirectory = context.RuntimeInfo.WorkingDirectory;
            currentEnv.Add("PYTHONPATH", JoinEnvironmentPaths(workingDirectory, packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        private string JoinEnvironmentPaths(params string[] paths)
        {
            return string.Join(Path.PathSeparator.ToString(), paths.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.Trim(System.IO.Path.PathSeparator)));
        }
        private string GetEntryModuleName(string entryFile)
        {
            //.py or .pyc
            string extName = System.IO.Path.GetExtension(entryFile);
            //.py or .pyc
            string nameWithOutExt = System.IO.Path.GetFileNameWithoutExtension(entryFile);

            return nameWithOutExt.Replace('/', '.')
                    .Replace('\\', '.')
                    .Trim('.');
        }
    }
}
