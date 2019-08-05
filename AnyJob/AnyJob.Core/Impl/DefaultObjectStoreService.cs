using AnyJob.DependencyInjection;
using System;
using System.IO;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultObjectStoreService : IObjectStoreService
    {
        ISerializeService serializeService;
        public DefaultObjectStoreService(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
        }
        public T GetObject<T>(string fileName)
        {
            try
            {
                string text = File.ReadAllText(fileName, Encoding.UTF8);
                return serializeService.Deserialize<T>(text);
            }
            catch (Exception ex)
            {
                throw Errors.GetObjectError(ex, fileName, typeof(T));
            }

        }

        public void SaveObject(object obj, string fileName)
        {
            try
            {
                string text = serializeService.Serialize(obj);
                File.WriteAllText(fileName, text, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw Errors.SaveObjectError(ex, obj, fileName);
            }

        }
    }
}
