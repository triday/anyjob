﻿using AnyJob.DependencyInjection;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultExpressionService : IExpressionService
    {
        public object Exec(string line, IDictionary<string, object> contexts)
        {
            var expression = DynamicExpressionParser.ParseLambda(typeof(object), line, contexts);
            return expression.Compile().DynamicInvoke();
        }
    }
}
