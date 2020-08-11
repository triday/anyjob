using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnyJob.Runner.Process;
namespace AnyJob.Runner.Java
{
    public class JavaAction : TypedProcessAction2
    {
        private readonly JavaOptions javaOptions;
        private readonly JavaEntryInfo javaEntryInfo;

        public JavaAction(JavaOptions option, JavaEntryInfo javaEntryInfo)
        {
            this.javaOptions = option;
            this.javaEntryInfo = javaEntryInfo;
        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            string classPaths = CombinClassPath(context);
            string wrapperPath = Path.GetFullPath(javaOptions.WrapperPath);
            return new ProcessExecInput
            {
                FileName = javaOptions.JavaPath,
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                Arguments = new string[] { "-cp", classPaths, "-jar", wrapperPath, javaEntryInfo.ClassFullName, javaEntryInfo.MethodName, inputFile, outputFile },
                Envs = new Dictionary<string, string>()
            };
        }
        private string CombinClassPath(IActionContext context)
        {
            var jars = Directory.GetFiles(context.RuntimeInfo.WorkingDirectory, "*.jar", SearchOption.AllDirectories);
            var allpaths = new string[] { "." }.Concat(jars);
            return JoinEnvironmentPaths(false, allpaths.ToArray());
        }

    }
}
