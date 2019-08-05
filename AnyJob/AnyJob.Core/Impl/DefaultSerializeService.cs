using AnyJob.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultSerializeService : ISerializeService
    {
        public string Serialize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                throw Errors.SerializeError(ex, obj);
            }
        }

        public T Deserialize<T>(string text)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(text);
            }
            catch (Exception ex)
            {
                throw Errors.DeserializeError(ex, typeof(T));
            }
        }

     
    }
}
