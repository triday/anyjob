using System;
using System.IO;
using System.Text;
using AnyJob.DependencyInjection;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultObjectStoreService : IObjectStoreService
    {
        static Encoding FileEncoding = new UTF8Encoding(false);
        ISerializeService serializeService;
        public DefaultObjectStoreService(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
        }
        public T GetObject<T>(string fileName)
        {
            try
            {
                string text = File.ReadAllText(fileName);
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
                File.WriteAllText(fileName, text, FileEncoding);
            }
            catch (Exception ex)
            {
                throw Errors.SaveObjectError(ex, obj, fileName);
            }

        }
    }
}
