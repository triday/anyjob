using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IServiceContainer:IServiceProvider
    {
        /// <summary>
        /// 根据泛型类型注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        void RegisteType<T>(T service) ;

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        void RegisteType<T>(Func<IServiceProvider, T> factory);
    }
}
