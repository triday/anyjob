using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass]

    public class DefaultFileStoreService : IObjectStoreService
    {
        ISerializeService serializeService;
        public DefaultFileStoreService(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
        }
        public T GetObject<T>(string fileName)
        {
            string text =File.ReadAllText(fileName, Encoding.UTF8);
            return serializeService.Deserialize<T>(text);
        }

        public void SaveObject(object obj, string fileName)
        {
            string text = serializeService.Serialize(obj);
            File.WriteAllText(fileName, text, Encoding.UTF8);
        }
    }
}
