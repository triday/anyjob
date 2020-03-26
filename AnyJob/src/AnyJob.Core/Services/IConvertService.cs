using System;

namespace AnyJob
{
    /// <summary>
    /// 表示类型转换服务
    /// </summary>
    public interface IConvertService
    {
        /// <summary>
        /// 将给定的值转换为指定的类型
        /// </summary>
        /// <param name="value">给定的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>返回转换后的目标类型的对象</returns>
        object Convert(object value, Type targetType);
    }
}
