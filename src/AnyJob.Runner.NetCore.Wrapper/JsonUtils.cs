using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AnyJob.Runner.NetCore.Wrapper
{
    class JsonUtils
    {
        static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static JsonDocument FromFile(string filePath)
        {
            string all = File.ReadAllText(filePath);
            return JsonDocument.Parse(all);
        }
        public static object Deserialize(string jsonText, Type type)
        {
            return JsonSerializer.Deserialize(jsonText, type, JsonSerializerOptions);
        }
        public static void WriteResultToFile(object result, string filePath)
        {
            string text = JsonSerializer.Serialize(result, JsonSerializerOptions);
            File.WriteAllText(filePath, text);
        }
    }
}
