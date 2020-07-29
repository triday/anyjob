using System;
using System.IO;
using AnyJob;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Runner.Process
{
    public abstract class TypedProcessAction2 : BaseProcessAction
    {
        const string ExchangeInputFileName = "input.exchange";
        const string ExchangeOutputFileName = "output.exchange";
        public override sealed object Run(IActionContext context)
        {
            var exchangePath = this.OnGetExchangeDirectory(context);
            try
            {
                System.IO.Directory.CreateDirectory(exchangePath);
                var inputFile = System.IO.Path.Combine(exchangePath, ExchangeInputFileName);
                var outputFile = System.IO.Path.Combine(exchangePath, ExchangeOutputFileName);
                this.WriteInputFile(context, inputFile);
                var execInputInfo = OnCreateExecInputInfo(context, exchangePath, inputFile, outputFile);
                var execOutputInfo = this.StartProcess(context, execInputInfo);
                return this.ReadOutputFile(context, outputFile);
            }
            finally
            {
                ClearExchangePath(exchangePath);
            }
        }
        protected virtual string OnGetExchangeDirectory(IActionContext context)
        {
            return Path.GetFullPath(Path.Combine("exchange", Guid.NewGuid().ToString()));
        }
        protected virtual void WriteInputFile(IActionContext context, string inputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            var objectStoreService = context.ServiceProvider.GetRequiredService<IObjectStoreService>();
            objectStoreService.SaveObject(context.Parameters.Arguments, inputFile);
        }

        private void ClearExchangePath(string exchangePath)
        {
            if (!Directory.Exists(exchangePath))
            {
                return;
            }
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

        protected abstract ProcessExecInput OnCreateExecInputInfo(IActionContext context, string exchangePath, string inputFile, string outputFile);

        protected virtual object ReadOutputFile(IActionContext context, string outputFile)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
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
