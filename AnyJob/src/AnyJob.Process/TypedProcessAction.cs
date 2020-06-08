using AnyJob;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace AnyJob.Process
{
    public abstract class TypedProcessAction : ProcessAction
    {
        const string RESULT_SPLIT_MAGIC_LINE = "***[[[!@#$%^&*()_!@#$%^&*(!@#$%^&]]]***";

        protected override object OnParseResult(IActionContext context, string output)
        {
            var index = output.IndexOf(RESULT_SPLIT_MAGIC_LINE);
            if (index < 0)
            {
                context.Output.WriteLine(output);
                throw ErrorFactory.FromCode(nameof(Errors.E80002));
            }
            else
            {
                string log = output.Substring(index).TrimEnd();
                string resultText = output.Substring(index + RESULT_SPLIT_MAGIC_LINE.Length).TrimStart();
                context.Output.WriteLine(log);
                var serializeService = context.ServiceProvider.GetService<ISerializeService>();
                var typedResult = serializeService.Deserialize<TypedProcessResult>(resultText);
                if (typedResult.Error != null)
                {
                    throw new TypedProcessException(typedResult.Error);
                }
                else
                {
                    return typedResult.Result;
                }
            }

        }
    }

    public abstract class TypedProcessAction2 : ProcessAction2
    {
        public override object Run(IActionContext context)
        {
            var exchange = this.CreateExchange(context);
            try
            {
                var execInputInfo = OnCreateExecInputInfo(context, exchange.ExchangePath, exchange.InputFile, exchange.OutputFile);
                var execOutputInfo = ProcessExecuter.Exec(execInputInfo);
                this.OnTraceLogger(context, execInputInfo, execOutputInfo);
                this.OnCheckProcessExecOutput(context, execInputInfo, execOutputInfo);
                return this.OnParseResult(context, execInputInfo, execOutputInfo);
            }
            finally
            {
                ClearExchangePath(exchange.ExchangePath);
            }
        }
        protected virtual (string ExchangePath, string InputFile, string OutputFile) CreateExchange(IActionContext context)
        {
            var objectStoreService = context.ServiceProvider.GetRequiredService<IObjectStoreService>();
            var exchangePath = System.IO.Path.GetTempPath();
            System.IO.Directory.CreateDirectory(exchangePath);
            var inputFile = System.IO.Path.Combine(exchangePath, "input.json");
            var outputFile = System.IO.Path.Combine(exchangePath, "output.json");
            objectStoreService.SaveObject(context.Parameters.Arguments, inputFile);
            return (exchangePath, inputFile, outputFile);
        }
        protected virtual void ClearExchangePath(string exchangePath)
        {
            foreach (var subDirectory in System.IO.Directory.GetDirectories(exchangePath))
            {
                ClearExchangePath(subDirectory);
            }
            foreach (var file in System.IO.Directory.GetFiles(exchangePath))
            {
                System.IO.File.Delete(file);
            }
            System.IO.Directory.Delete(exchangePath);
        }

        protected virtual ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile)
        {
            var (fileName, arguments, stdInput, envs) = this.OnGetStartInfo(context, exchangePath, inputFile, outputFile);
            return new ProcessExecInput
            {
                WorkingDir = context.RuntimeInfo.WorkingDirectory,
                StandardInput = stdInput,
                Envs = envs,
                Arguments = arguments ?? new string[0],
                FileName = fileName
            };
        }
        protected abstract (string FileName, string[] Arguments, string StandardInput, IDictionary<string, string> EnvironmentVariables) OnGetStartInfo(IActionContext context, string exchangePath, string inputFile, string outputFile);
        protected override object OnParseResult(IActionContext context, ProcessExecInput input, ProcessExecOutput output)
        {
            var outputFile = input.Arguments.Last();
            var inputFile = input.Arguments.Reverse().Skip(1).First();
            var objectStoreService = context.ServiceProvider.GetRequiredService<IObjectStoreService>();
            var typedResult = objectStoreService.GetObject<TypedProcessResult>(outputFile);
            if (typedResult.Error != null)
            {
                throw new TypedProcessException(typedResult.Error);
            }
            else
            {
                return typedResult.Result;
            }
        }
    }
}
