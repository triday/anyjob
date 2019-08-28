using System.Collections.Generic;

namespace AnyJob
{
    /// <summary>
    /// 表示动作的参数类型
    /// </summary>
    public interface IActionType
    {
        /// <summary>
        /// 获取默认值
        /// </summary>
        object DefaultValue { get; }
        /// <summary>
        /// 获取或设置一个值，该值表示是否属于必须参数
        /// </summary>
        bool IsMust { get; }
        /// <summary>
        /// 验证给定的值是否满足要求
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <param name="errorMessages">错误信息列表</param>
        /// <returns>返回一个值，该值表示是否验证成功</returns>
        bool Validate(object value, out IList<string> errorMessages);
        /// <summary>
        /// 将保密字段星号化
        /// </summary>
        /// <param name="value">输入的值</param>
        /// <returns></returns>
        object MaskSecret(object value);
    }
}
