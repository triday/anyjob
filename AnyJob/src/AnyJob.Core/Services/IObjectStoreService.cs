using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示对象的本地存储服务
    /// </summary>
    public interface IObjectStoreService
    {
        /// <summary>
        /// 保存对象到指定的路径
        /// </summary>
        /// <param name="obj">要保存的对象</param>
        /// <param name="fileName">指定的路径</param>
        void SaveObject(object obj, string fileName);
        /// <summary>
        /// 从指定的路径读取对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="fileName">指定的路径</param>
        /// <returns>返回读取到的对象</returns>
        T GetObject<T>(string fileName);
    }
}
