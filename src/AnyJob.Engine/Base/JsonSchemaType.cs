using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace AnyJob
{
    public class JsonSchemaType : IActionType
    {
        static JToken MaskStringValue = JToken.FromObject("******");
        static JToken MaskIntegerValue = JToken.FromObject(0);
        static JToken MaskNumberValue = JToken.FromObject(0.0);
        public JsonSchemaType(JSchema jSchema)
        {
            if (jSchema.ExtensionData.TryGetValue("must", out var token))
            {
                this.IsMust = token.ToObject<bool>();
            }
            if (jSchema.Default != null)
            {
                this.DefaultValue = jSchema.Default.ToObject<object>();
            }
            this.Schema = jSchema;
        }
        public JSchema Schema { get; private set; }

        public bool IsMust { get; private set; }

        public object DefaultValue { get; private set; }

        public bool Validate(object value, out IList<string> errorMessages)
        {
            JToken jToken = JToken.FromObject(value);
            return jToken.IsValid(this.Schema, out errorMessages);
        }

        public object MaskSecret(object value)
        {
            return MaskWriteOnlyProperties(this.Schema, JToken.FromObject(value));
        }
        private JToken MaskWriteOnlyProperties(JSchema schema, JToken token)
        {
            if (schema == null || token == null) return token;
            if (!schema.Type.HasValue) return token;
            if (token is JObject && schema.Type.Value == JSchemaType.Object)
            {
                JObject newObject = new JObject();
                foreach (var prop in token as JObject)
                {
                    if (schema.Properties.TryGetValue(prop.Key, out var propSchema))
                    {
                        newObject[prop.Key] = MaskWriteOnlyProperties(propSchema, prop.Value);
                    }
                    else
                    {
                        newObject[prop.Key] = prop.Value;
                    }
                }
                return newObject;

            }
            else if (token is JArray && schema.Type.Value == JSchemaType.Array)
            {
                var itemSchema = schema.Items.FirstOrDefault();
                if (itemSchema == null) return token;
                return new JArray(token.Select(p => MaskWriteOnlyProperties(itemSchema, p)).ToArray());
            }
            else if (schema.WriteOnly.HasValue && schema.WriteOnly.Value)
            {
                return MaskWriteOnlyValue(token);
            }
            else
            {
                return token;
            }
        }
        private JToken MaskWriteOnlyValue(JToken token)
        {
            if (token.Type == JTokenType.String)
            {
                return MaskStringValue;
            }
            else if (token.Type == JTokenType.Float)
            {
                return MaskNumberValue;
            }
            else if (token.Type == JTokenType.Integer)
            {
                return MaskIntegerValue;
            }
            else
            {
                return token;
            }
        }
    }
}
