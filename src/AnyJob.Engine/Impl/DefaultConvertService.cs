using System;
using Newtonsoft.Json.Linq;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultConvertService : IConvertService
    {
        public object Convert(object value, Type targetType)
        {
            _ = targetType ?? throw new ArgumentNullException(nameof(targetType));
            try
            {
                if (value != null && targetType.IsAssignableFrom(value.GetType()))
                {
                    return value;
                }
                var jtoken = JToken.FromObject(value);
                return jtoken.ToObject(targetType);
            }
            catch (Exception ex)
            {
                throw Errors.ConvertError(ex, value, targetType);
            }
        }
    }
}
