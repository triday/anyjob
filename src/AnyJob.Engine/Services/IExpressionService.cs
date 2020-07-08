using System.Collections.Generic;

namespace AnyJob
{
    /// <summary>
    /// 表达式动态求值
    /// </summary>
    public interface IExpressionService
    {
        /// <summary>
        /// 计算表达式的值
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="contexts">参数值</param>
        /// <returns>返回计算后的值</returns>
        object Exec(string expression, IDictionary<string, object> contexts);
    }
}
