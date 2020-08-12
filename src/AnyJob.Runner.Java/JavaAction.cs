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
            string classPaths = CombinClassPath(context.RuntimeInfo.WorkingDirectory, Path.GetFullPath(javaOptions.GlobalJarLibsPath));

            return new ProcessExecInput
            {
                FileName = javaOptions.JavaPath,
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                Arguments = new string[] { "-cp", classPaths, javaOptions.EntryClass, javaEntryInfo.ClassFullName, javaEntryInfo.MethodName, inputFile, outputFile },
                Envs = new Dictionary<string, string>()
            };
        }
        private string CombinClassPath(string packJarLibsPath, string globalJarLibsPath)
        {
            var pack_jars = Directory.GetFiles(packJarLibsPath, "*.jar", SearchOption.AllDirectories);
            var global_jars = Directory.GetFiles(globalJarLibsPath, "*.jar", SearchOption.AllDirectories);
            var allpaths = new string[] { "." }.Concat(pack_jars).Concat(global_jars);
            return JoinEnvironmentPaths(false, allpaths.ToArray());
        }

    }
}
