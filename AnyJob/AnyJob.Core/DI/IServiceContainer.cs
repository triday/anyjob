using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.DI
{

    public interface IServiceContainer:IServiceProvider
    {
        /// <summary>
        /// 根据泛型类型注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        void RegisteService<T>(T service) ;


    }
}
