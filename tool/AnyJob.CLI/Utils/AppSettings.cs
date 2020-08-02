using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace AnyJob.CLI.Utils
{
    public class AppSettings
    {
        public static void MergeOptions(string settingFile, object options)
        {
            if (options == null) return;
            string content = File.Exists(settingFile) ? File.ReadAllText(settingFile) : "{}";
            JObject jObject = JObject.Parse(content);
            var attr = options.GetType().GetCustomAttribute<YS.Knife.OptionsClassAttribute>();
            if (attr != null)
            {
                jObject[attr.ConfigKey] = JObject.FromObject(options);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(settingFile));
            using (StreamWriter file = File.CreateText(settingFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.IndentChar = ' ';
                jObject.WriteTo(writer);
            }
        }
        public static void MergeOptions(object options)
        {
            string userSettingFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".anyjob", "appsettings.json");
            MergeOptions(userSettingFile, options);

        }
    }
}
