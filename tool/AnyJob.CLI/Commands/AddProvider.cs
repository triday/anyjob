using System.Drawing;
using AnyJob.Config;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Console = Colorful.Console;
namespace AnyJob.CLI.Commands
{

    [Verb("add-provider", HelpText = "Add a new package provider.")]
    public class AddProvider : ICommand
    {

        [Value(1, MetaName = "name", Required = true, HelpText = "provider name")]
        public string Name { get; set; }
        [Value(2, MetaName = "address", Required = true, HelpText = "provider url address.")]
        public string Address { get; set; }
        public int Run()
        {
            var packOptions = JobHost.Instance.GetRequiredService<PackOption>();
            if (packOptions.Providers.ContainsKey(this.Name))
            {
                Console.WriteFormatted("The name '{0}' already exists.", Color.White, new Colorful.Formatter(this.Name, Color.Yellow));
                return 1;
            }
            packOptions.Providers[Name] = Address;
            Utils.AppSettings.MergeOptions(packOptions);
            return 0;
        }
    }
}
