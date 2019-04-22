using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.DI
{
    public interface IInstanceProvider
    {
        /// <summary>
        /// 根据泛型类型获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetInstance<T>();
    }
}
