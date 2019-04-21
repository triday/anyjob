using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 提供获取服务的接口
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// 根据泛型类型获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetInstance<T>();

        
    }
}
