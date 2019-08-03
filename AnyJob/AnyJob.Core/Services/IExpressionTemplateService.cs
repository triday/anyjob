namespace AnyJob
{
    /// <summary>
    /// 表示表达式模板的服务
    /// </summary>
    public interface IExpressionTemplateService
    {
        /// <summary>
        /// 获取一个值，该值表示指定的字符串是否是模板字符串
        /// </summary>
        /// <param name="text">要判定的字符串</param>
        /// <returns>返回是否是模板字符串</returns>
        bool IsExpression(string text);
        /// <summary>
        /// 从指定的模板字符串中提取表达式
        /// </summary>
        /// <param name="text">模板字符串</param>
        /// <returns>返回表达式字符串</returns>
        string PickExpression(string text);
    }
}
