using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AnyJob.CLI.Commands
{
    [Verb("stop", HelpText = "检查")]
    public class StopOption : ICommand
    {
        public int Run()
        {
            Console.WriteLine("stop ...");
            return 0;
        }
    }
}
