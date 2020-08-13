using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnyJob.Runner.Process;

namespace AnyJob.Runner.Java
{
    public class DockerJavaAction : TypedProcessAction2
    {
        private readonly JavaOptions javaOptions;
        private readonly JavaEntryInfo javaEntryInfo;

        public DockerJavaAction(JavaOptions option, JavaEntryInfo javaEntryInfo)
        {
            this.javaOptions = option;
            this.javaEntryInfo = javaEntryInfo;
        }

        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            string RootDirInDocker = "/anyjob";
            string PackageDirInDocker = System.IO.Path.Combine(RootDirInDocker, "packs", context.Name.Pack).ToUnixPath();

            string globalJarDirInLocal = Path.GetFullPath(Path.GetDirectoryName(javaOptions.GlobalJarLibsPath));
            string globalJarDirInDocker = Path.Combine(RootDirInDocker, "global/java/").ToUnixPath();


            string exchangePathInDocker = Path.Combine(RootDirInDocker, "exchange").ToUnixPath();
            string inputFileInDocker = Path.Combine(exchangePathInDocker, Path.GetFileName(inputFile)).ToUnixPath();
            string outputFileInDocker = Path.Combine(exchangePathInDocker, Path.GetFileName(outputFile)).ToUnixPath();

            String classPathsInDocker = CombinClassPathInDocker(context.RuntimeInfo.WorkingDirectory, PackageDirInDocker, globalJarDirInLocal, globalJarDirInDocker);

            return ProcessExecuter.BuildDockerProcess(javaOptions.DockerImage,
                new[] { javaOptions.JavaPath, "-cp", classPathsInDocker, javaOptions.EntryClass, javaEntryInfo.ClassFullName, javaEntryInfo.MethodName, inputFileInDocker, outputFileInDocker },
                PackageDirInDocker,
                new Dictionary<string, string>
                {
                    [context.RuntimeInfo.WorkingDirectory] = PackageDirInDocker,
                    [globalJarDirInLocal] = globalJarDirInDocker,
                    [exchangePath] = exchangePathInDocker
                },
                new Dictionary<string, string>
                {
                },
                string.Empty
                );
        }
        private string CombinClassPathInDocker(string workingDirectoryInLocal, string workingDirectoryInDocker, string globarJarDirInLocal, string globarJarDirInDocker)
        {
            var pack_jars = Directory.GetFiles(workingDirectoryInLocal, "*.jar", SearchOption.AllDirectories)
                    .Select(full => full.Substring(workingDirectoryInLocal.Length).TrimStart(Path.DirectorySeparatorChar))
                    .Select(relative => Path.Combine(workingDirectoryInDocker, relative));


            var global_jars = Directory.GetFiles(globarJarDirInLocal, "*.jar", SearchOption.AllDirectories)
                .Select(full => full.Substring(globarJarDirInLocal.Length).TrimStart(Path.DirectorySeparatorChar))
                .Select(relative => Path.Combine(globarJarDirInDocker, relative));
            var allpaths = new string[] { "." }.Concat(pack_jars).Concat(global_jars);
            return JoinEnvironmentPaths(true, allpaths.ToArray());
        }
    }
}
