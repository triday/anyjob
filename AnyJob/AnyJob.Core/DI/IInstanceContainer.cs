using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.DI
{
    public interface IInstanceContainer:IInstanceProvider
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        void RegisteInstance<T>(Func<IServiceProvider, T> factory);
    }
}
