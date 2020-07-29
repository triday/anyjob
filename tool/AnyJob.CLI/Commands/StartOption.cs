using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AnyJob.CLI.Commands
{
    [Verb("start", HelpText = "start provider.")]
    public class StartOption : ICommand
    {
        public string ProviderName { get; set; }
        public int Port { get; set; }
        public string LocalDir { get; set; }

        public int Run()
        {
            Console.WriteLine("start ...");
            return 0;
        }
    }
}
