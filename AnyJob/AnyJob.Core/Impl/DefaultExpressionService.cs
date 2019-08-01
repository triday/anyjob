using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IExpressionService))]
    public class DefaultExpressionService : IExpressionService
    {
        public object Exec(string line, IDictionary<string, object> contexts)
        {
            var expression = DynamicExpressionParser.ParseLambda(typeof(object), line, contexts);
            return expression.Compile().DynamicInvoke();
        }
    }
}
