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
            var index = output.IndexOf(RESULT_SPLIT_MAGIC_LINE);
            if (index < 0)
            {
                context.Logger.WriteLine(output);
                throw ErrorFactory.FromCode(nameof(Errors.E80002));
            }
            else
            {
                string log = output.Substring(index).TrimEnd();
                string resultText = output.Substring(index + RESULT_SPLIT_MAGIC_LINE.Length).TrimStart();
                context.Logger.WriteLine(log);
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
}
