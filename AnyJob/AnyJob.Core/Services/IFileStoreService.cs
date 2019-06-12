using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IFileStoreService
    {
        void WriteObject(object obj, string fileName);
        T ReadObject<T>(string fileName);
    }
}
