using AnyJob.CLI.Utils;
using CommandLine;

namespace AnyJob.CLI.Commands
{
    [Verb("start-provider", HelpText = "Start a local provider provider.")]
    public class StartProvider : ICommand
    {
        [Option("port", Required = false, Default = 80, HelpText = "service port")]
        public int ServicePort { get; set; } = 80;

        [Value(1, MetaName = "root-folder", Required = true, HelpText = "Service root folder.")]
        public string RootFolder { get; set; }
        [Option("name", Required = false, Default = "anyjob-package-provider", HelpText = "docker container name")]
        public string ContainerName { get; set; } = "anyjob-package-provider";
        public int Run()
        {
            return Shell.Exec("docker", $"run --rm --name {ContainerName} -d -p {ServicePort}:80 -v {RootFolder}:/anyjob/packs anyjob/anyjob-file-provider:latest");
        }
    }
}
