using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Process
{
    public abstract class TypedProcessAction : ProcessAction
    {
        const string RESULT_SPLIT_MAGIC_LINE = "***[[[!@#$%^&*()_!@#$%^&*(!@#$%^&]]]***";

        protected override object OnParseResult(IActionContext context, string output)
        {
            string[] items = output.Split(RESULT_SPLIT_MAGIC_LINE);
            string log = items[0];
            string resultText = items[1];
            context.Logger.WriteLine(log);
            var serializeService = context.ServiceProvider.GetService<ISerializeService>();
            return serializeService.Deserialize(resultText, typeof(object));

        }
    }
}
