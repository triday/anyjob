using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultDynamicValueService : IDynamicValueService
    {
        public DefaultDynamicValueService(IExpressionTemplateService expressionTemplateService, IExpressionService expressionService)
        {
            this.expressionTemplateService = expressionTemplateService;
            this.expressionService = expressionService;
        }
        private IExpressionTemplateService expressionTemplateService;
        private IExpressionService expressionService;
        public object GetDynamicValue(object value, IActionParameter actionParameter)
        {
            _ = actionParameter ?? throw new ArgumentNullException(nameof(actionParameter));
            try
            {
                if (value == null) return null;
                if (value is string)
                {
                    return GetStringDynamicValue(value as string, actionParameter);
                }
                else if (value is JArray)
                {
                    return GetJArrayDynamicValue(value as JArray, actionParameter);
                }
                else if (value is JObject)
                {
                    return GetJObjectDynamicValue(value as JObject, actionParameter);
                }
                else
                {
                    return value;
                }
            }
            catch (Exception ex)
            {
                throw Errors.GetDynamicValueError(ex);
            }

        }
        private object GetStringDynamicValue(string text, IActionParameter actionParameter)
        {
            if (expressionTemplateService.IsExpression(text))
            {
                var expression = expressionTemplateService.PickExpression(text);
                return expressionService.Exec(expression, actionParameter.GetAllValues());
            }
            else
            {
                return text;
            }
        }
        private object GetJArrayDynamicValue(JArray jArray, IActionParameter actionParameter)
        {
            return new JArray(jArray.Select(p => GetDynamicValue(p, actionParameter)).ToArray());
        }
        private object GetJObjectDynamicValue(JObject jObject, IActionParameter actionParameter)
        {
            var result = new JObject();
            foreach (var prop in jObject)
            {
                object dynamicKey = GetDynamicValue(prop.Key, actionParameter);
                object dynamicValue = GetDynamicValue(prop.Value, actionParameter);
                result[dynamicKey] = JToken.FromObject(dynamicValue);
            }
            return result;
        }
    }
}
