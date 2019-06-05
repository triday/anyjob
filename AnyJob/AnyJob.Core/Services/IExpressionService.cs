using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表达式动态求值
    /// </summary>
    public interface IExpressionService
    {
        object Exec(string line, Dictionary<string,object> valueProvider);
    }
}
