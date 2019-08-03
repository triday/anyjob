using AnyJob.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass]
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
            if (value == null) return null;
            if (value is string)
            {
                if (expressionTemplateService.IsExpression(value as string))
                {
                    var expression = expressionTemplateService.PickExpression(value as string);
                    return expressionService.Exec(expression, actionParameter.GetAllValues());
                }
                else
                {
                    return value;
                }
            }
            else if (value is Array)
            {
                Array valueArray = value as Array;
                object[] resultArray = new object[valueArray.Length];
                for (int i = 0; i < resultArray.Length; i++)
                {
                    resultArray[i] = GetDynamicValue(valueArray.GetValue(i), actionParameter);
                }
                return resultArray;
            }
            else if (value is IDictionary)
            {
                IDictionary valueDictionary = value as IDictionary;
                Dictionary<object, object> resultDictionary = new Dictionary<object, object>();
                foreach (var key in valueDictionary.Keys)
                {
                    object dynamicKey = GetDynamicValue(key, actionParameter);
                    object dynamicValue = GetDynamicValue(valueDictionary[key], actionParameter);
                    resultDictionary[dynamicKey] = dynamicValue;
                }
                return resultDictionary;
            }
            else
            {
                return value;
            }
        }
    }
}
