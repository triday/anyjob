namespace AnyJob
{
    /// <summary>
    /// 表示动态值的服务
    /// </summary>
    public interface IDynamicValueService
    {
        /// <summary>
        /// 获取动态值
        /// </summary>
        /// <param name="value">传入的值</param>
        /// <param name="actionParameter">参数信息</param>
        /// <returns>返回动态的值</returns>
        object GetDynamicValue(object value, IActionParameter actionParameter);
    }
}
