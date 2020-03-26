using AnyJob.DependencyInjection;
using AnyJob.Workflow.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow.Impl
{
    [ServiceImplClass]
    public class DefaultPublishValueService : IPublishValueService
    {
        public DefaultPublishValueService(IDynamicValueService dynamicValueService, IConvertService convertService)
        {
            this.dynamicValueService = dynamicValueService;
            this.convertService = convertService;
        }
        private IConvertService convertService;
        private IDynamicValueService dynamicValueService;
        public void PublishGlobalVars(string key, object value, IActionParameter actionParameters)
        {
            object dynamicKey = dynamicValueService.GetDynamicValue(key, actionParameters);
            string dynamicKeyText = (string)convertService.Convert(dynamicKey, typeof(string));
            object dynamicValue = dynamicValueService.GetDynamicValue(value, actionParameters);
            actionParameters.GlobalVars[dynamicKeyText] = dynamicValue;
        }

        public void PublishVars(string key, object value, IActionParameter actionParameters)
        {
            object dynamicKey = dynamicValueService.GetDynamicValue(key, actionParameters);
            string dynamicKeyText = (string)convertService.Convert(dynamicKey, typeof(string));
            object dynamicValue = dynamicValueService.GetDynamicValue(value, actionParameters);
            actionParameters.Vars[dynamicKeyText] = dynamicValue;
        }
    }
}
