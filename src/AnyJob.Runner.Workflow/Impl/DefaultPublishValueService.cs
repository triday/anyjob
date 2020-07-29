using System;
using AnyJob.Runner.Workflow.Services;

namespace AnyJob.Runner.Workflow.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
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
            _ = actionParameters ?? throw new ArgumentNullException(nameof(actionParameters));
            object dynamicKey = dynamicValueService.GetDynamicValue(key, actionParameters);
            string dynamicKeyText = (string)convertService.Convert(dynamicKey, typeof(string));
            object dynamicValue = dynamicValueService.GetDynamicValue(value, actionParameters);
            actionParameters.GlobalVars[dynamicKeyText] = dynamicValue;
        }

        public void PublishVars(string key, object value, IActionParameter actionParameters)
        {
            _ = actionParameters ?? throw new ArgumentNullException(nameof(actionParameters));
            object dynamicKey = dynamicValueService.GetDynamicValue(key, actionParameters);
            string dynamicKeyText = (string)convertService.Convert(dynamicKey, typeof(string));
            object dynamicValue = dynamicValueService.GetDynamicValue(value, actionParameters);
            actionParameters.Vars[dynamicKeyText] = dynamicValue;
        }
    }
}
