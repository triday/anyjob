using System;

namespace AnyJob
{
    /// <summary>
    /// 表示时间服务
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// 获取当前的时间
        /// </summary>
        /// <returns>返回当前的时间</returns>
        DateTimeOffset Now();
    }
}
