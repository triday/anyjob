using System.Drawing;
using System.Linq;
using CommandLine;
using Console = Colorful.Console;
namespace AnyJob.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteAscii("ANYJOB", Color.DarkCyan);
            System.Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "abc");
            var commands = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsClass && typeof(ICommand).IsAssignableFrom(p)).ToArray();
            return Parser.Default.ParseArguments(args, commands).MapResult(command => (command as ICommand).Run(), errors => 1);

        }
    }
}
