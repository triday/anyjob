using System.Linq;
using AnyJob.Config;
using CommandLine;
using ConsoleTables;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.CLI.Commands
{
    [Verb("list-provider", HelpText = "List all providers.")]
    public class ListProvider : ICommand
    {
        public int Run()
        {
            var packOptions = JobHost.Instance.GetRequiredService<PackOption>();
            var table = new ConsoleTable("name", "address");
            packOptions.Providers.ToList().ForEach(kv =>
            {
                string name = kv.Key == packOptions.DefaultProviderName ? $"{kv.Key}(*)" : kv.Key;
                table.AddRow(name, kv.Value);
            });

            table.Write(Format.Alternative);
            return 0;
        }
    }
}
