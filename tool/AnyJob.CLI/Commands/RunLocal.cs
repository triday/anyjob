using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.CLI.Commands
{
    [Verb("run-local", HelpText = "Run a job in local machine.")]
    public class RunLocal : ICommand
    {
        [Value(1, MetaName = "action", Required = true, HelpText = "Action full name.")]
        public string ActionName { get; set; }
        [Value(2, MetaName = "args", Required = false, Default = "{}", HelpText = "Action full name.")]
        public string JsonArgs { get; set; } = "{}";
        public int Run()
        {
            var serializeService = JobHost.Instance.GetRequiredService<ISerializeService>();
            var args = serializeService.Deserialize<Dictionary<string, object>>(JsonArgs);
            var startTime = DateTimeOffset.UtcNow;
            var job = JobEngine.Start(ActionName, args);
            var result = job.Task.Result;

            if (result.IsSuccess)
            {
                WriteSuccessResult(startTime, result.Result);
            }
            else
            {
                WriteException(startTime, result.ExecuteError);
            }
            return 0;
        }
        private void WriteSuccessResult(DateTimeOffset startTime, object result)
        {
            var totalTime = DateTime.UtcNow - startTime;
            // wait other task write console done.
            Task.Delay(500).Wait();
            var serializeService = JobHost.Instance.GetRequiredService<ISerializeService>();
            Console.WriteLine();
            Console.WriteLine("The job was successfully executed.");
            Console.WriteLine("Execution duration: {0:f2} seconds", totalTime.TotalSeconds);
            Console.WriteLine("Result: {0}", serializeService.Serialize(result));
            Console.WriteLine();
        }
        private void WriteException(DateTimeOffset startTime, Exception exception)
        {
            var totalTime = DateTime.UtcNow - startTime;
            // wait other task write console done.
            Task.Delay(500).Wait();

            Console.WriteLine();
            Console.WriteLine("Job execution failed.");
            Console.WriteLine("Execution duration: {0:f2} seconds", totalTime.TotalSeconds);
            Console.WriteLine("Exception: {0}", exception.Message);
            Console.WriteLine();
        }
    }
}
