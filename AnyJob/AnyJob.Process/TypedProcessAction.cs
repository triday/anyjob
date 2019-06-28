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
            if (items.Length != 2)
            {
                context.Logger.WriteLine(output);
                throw ErrorFactory.FromCode(nameof(Errors.E80002));
            }
            else
            {
                string log = items[0].TrimEnd();
                string resultText = items[1].TrimStart();
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
