using AnyJob.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(ISerializeService))]
    public class DefaultSerializeService : ISerializeService
    {
        public string Kind
        {
            get
            {
                return "json";
            }
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public object Deserialize(string text, Type type)
        {
            return JsonConvert.DeserializeObject(text, type);
        }
    }
}
