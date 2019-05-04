using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ISerializeService
    {
        string Serialize(object obj);
        T Deserialize<T>(string text);
    }
}
