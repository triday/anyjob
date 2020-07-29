﻿using System;
using System.Collections.Generic;
using System.IO;
using AnyJob.Runner.Process;

namespace AnyJob.Runner.Python
{
    public class PythonAction : TypedProcessAction2
    {
        public PythonAction(PythonOption pythonOption, string entryFile)
        {
            this.pythonOption = pythonOption;
            this.entryFile = entryFile;
        }

        private PythonOption pythonOption;
        private string entryFile;


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
        protected IDictionary<string, string> OnGetEnvironment(IActionContext context, bool inDocker)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            var currentEnv = new Dictionary<string, string>();
            string currentNodeModulesPath = System.Environment.GetEnvironmentVariable("PYTHONPATH");
            string packNodeModulesPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(context.RuntimeInfo.WorkingDirectory, pythonOption.PackPythonLibPath));
            string globalNodeModulesPath = System.IO.Path.GetFullPath(pythonOption.GlobalPythonLibPath);
            string workingDirectory = context.RuntimeInfo.WorkingDirectory;
            currentEnv.Add("PYTHONPATH", JoinEnvironmentPaths(inDocker, workingDirectory, packNodeModulesPath, globalNodeModulesPath, currentNodeModulesPath));
            return currentEnv;
        }
        protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            return InDocker ?
                CreateDockerInputInfo(context, exchangePath, inputFile, outputFile) :
                CreateLocalInputInfo(context, exchangePath, inputFile, outputFile);

        }
        private ProcessExecInput CreateLocalInputInfo(IActionContext context, string _, string inputFile, string outputFile)
        {
            string wrapperPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, pythonOption.WrapperPath));
            string entryModule = this.GetEntryModuleName(this.entryFile);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                FileName = pythonOption.PythonPath,
                StandardInput = string.Empty,
                Arguments = new string[] { wrapperPath, entryModule, inputFile, outputFile },
                Envs = this.OnGetEnvironment(context, false),
            };
        }
        private ProcessExecInput CreateDockerInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            string RootDirInDocker = "/anyjob";
            string PackageDirInDocker = System.IO.Path.Combine(RootDirInDocker, "packs", context.Name.Pack).ToUnixPath();
            string wrapperPathInDocker = System.IO.Path.Combine(RootDirInDocker, "python_wrapper.py").ToUnixPath();
            string globalLibDirInLocal = System.IO.Path.GetFullPath(pythonOption.GlobalPythonLibPath);
            string globalLibDirInDocker = System.IO.Path.Combine(RootDirInDocker, pythonOption.GlobalPythonLibPath).ToUnixPath();
            string exchangePathInDocker = System.IO.Path.Combine(RootDirInDocker, "exchange").ToUnixPath();
            string inputFileInDocker = System.IO.Path.Combine(exchangePathInDocker, Path.GetFileName(inputFile)).ToUnixPath();
            string outputFileInDocker = System.IO.Path.Combine(exchangePathInDocker, Path.GetFileName(outputFile)).ToUnixPath();

            string wrapperPathInLocal = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, pythonOption.WrapperPath));
            string entryModule = this.GetEntryModuleName(this.entryFile);
            string packNodeModulesPathInDocker = System.IO.Path.Combine(PackageDirInDocker, pythonOption.PackPythonLibPath).ToUnixPath();

            return ProcessExecuter.BuildDockerProcess(
                pythonOption.DockerImage,
                new string[] { pythonOption.PythonPath, wrapperPathInDocker, entryModule, inputFileInDocker, outputFileInDocker },
                PackageDirInDocker,
                new Dictionary<string, string>
                {
                    [context.RuntimeInfo.WorkingDirectory] = PackageDirInDocker,
                    [wrapperPathInLocal] = wrapperPathInDocker,
                    [exchangePath] = exchangePathInDocker,
                    [globalLibDirInLocal] = globalLibDirInDocker
                },
                new Dictionary<string, string>
                {
                    ["PYTHONPATH"] = JoinEnvironmentPaths(true, PackageDirInDocker, packNodeModulesPathInDocker, globalLibDirInDocker)
                },
                string.Empty);

        }
        private bool InDocker
        {
            get
            {
                return !string.IsNullOrEmpty(this.pythonOption.DockerImage);
            }
        }
    }
}
