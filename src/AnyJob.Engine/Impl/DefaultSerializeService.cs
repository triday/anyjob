using System;
using System.IO;
using AnyJob.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
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
                JObject obj = JObject.Parse(text);
                var schema = GetJSchema(typeof(T));
                if (schema != null && !obj.IsValid(schema))
                {
                    throw Errors.InvalidJsonSchema(typeof(T));

                }
                return obj.ToObject<T>();
            }
            catch (Exception ex)
            {
                throw Errors.DeserializeError(ex, typeof(T));
            }
        }

        public JSchema GetJSchema(Type type)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));
            var schemaAttr = Attribute.GetCustomAttribute(type, typeof(SchemaAttribute)) as SchemaAttribute;
            if (schemaAttr == null) return null;
            using (var stream = type.Assembly.GetManifestResourceStream(schemaAttr.SchemaFile))
            {
                using (var textReader = new StreamReader(stream, System.Text.Encoding.UTF8))
                {
                    using (JsonReader jsonReader = new JsonTextReader(textReader))
                    {
                        return JSchema.Load(jsonReader);
                    }
                }
            }
        }
    }
}
