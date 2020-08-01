using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using AnyJob.Config;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Console = Colorful.Console;
namespace AnyJob.CLI.Commands
{
    [Verb("remove-provider", HelpText = "Remove an exists package provider.")]
    public class RemoveProvider : ICommand
    {
        [Option("name", Required = true, HelpText = "provider name")]
        public string Name { get; set; }

        public int Run()
        {
            var packOptions = JobHost.Instance.GetRequiredService<PackOption>();

            if (packOptions.Providers.Remove(this.Name))
            {
                Utils.AppSettings.MergeOptions(packOptions);
                Console.WriteFormatted("The provider {0} was removed.", Color.Green, Color.White, this.Name);
            }
            else
            {
                Console.WriteFormatted("The provider {0} was not found.", Color.Red, Color.White, this.Name);
            }
            return 0;
        }
    }
}
