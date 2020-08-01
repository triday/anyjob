using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.CLI.Utils;
using CommandLine;

namespace AnyJob.CLI.Commands
{
    [Verb("stop-provider", HelpText = "Stop local provider provider.")]
    public class StopProvider : ICommand
    {
        [Option("name", Required = false, Default = "anyjob-package-provider", HelpText = "docker container name")]
        public string ContainerName { get; set; } = "anyjob-package-provider";
        public int Run()
        {
            return Shell.Exec("docker", $"stop {ContainerName}");
        }
    }
}
