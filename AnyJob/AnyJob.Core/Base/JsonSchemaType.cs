using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class JsonSchemaType : IActionType
    {
        public JsonSchemaType(JSchema jSchema)
        {
            if (jSchema.ExtensionData.TryGetValue("must", out var token))
            {
                this.IsMust = token.ToObject<bool>();
            }
            //jSchema.AdditionalProperties.Properties
            //JToken isMustNode = jSchema.SelectToken("ismust");
            //this.IsMust = isMustNode!=null?isMustNode.ToObject<bool>():false;
            this.Schema = jSchema;


        }
        public JSchema Schema { get; private set; }

        public bool IsMust { get; private set; }

        public object Default
        {
            get
            {
                return this.Schema.Default;
            }
        }

        public object Protect(object value)
        {
            bool isStringType = Schema.Type.HasValue && Schema.Type.Value == JSchemaType.String;
            bool isWriteOnly = Schema.WriteOnly.HasValue && Schema.WriteOnly.Value;
            if (isStringType && isWriteOnly)
            {
                return "******";
            }
            else
            {
                return value;
            }
        }

        public bool Validate(object value, out IList<string> errorMessages)
        {
            JToken jToken = JToken.FromObject(value);
            return jToken.IsValid(this.Schema, out errorMessages);
        }
    }
}
