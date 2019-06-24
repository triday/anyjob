using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    public class DefaultExpressionService : IExpressionService
    {
        public object Exec(string line, IDictionary<string, object> contexts)
        {
            //System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda()
            throw new NotImplementedException();
        }
    }
}
