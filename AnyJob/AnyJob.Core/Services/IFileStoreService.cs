using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    /// <summary>
    /// 表示对象的本地存储服务
    /// </summary>
    public interface IFileStoreService
    {
        void WriteObject(object obj, string fileName);
        T ReadObject<T>(string fileName);
    }
}
