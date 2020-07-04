using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using AnyJob.DependencyInjection;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultExpressionService : IExpressionService
    {
        public object Exec(string line, IDictionary<string, object> contexts)
        {
            try
            {
                var expression = DynamicExpressionParser.ParseLambda(typeof(object), line, contexts);
                return expression.Compile().DynamicInvoke();
            }
            catch (System.Exception ex)
            {
                throw Errors.CalcExpressionValueError(ex, line);
            }
        }
    }
}
