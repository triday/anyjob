using AnyJob.DependencyInjection;
using System;

namespace AnyJob.Impl
{
    [ServiceImplClass]
    public class DefaultConvertService : IConvertService
    {
        public object Convert(object value, Type targetType)
        {
            try
            {
                return System.Convert.ChangeType(value, targetType);
            }
            catch (Exception ex)
            {
                throw Errors.ConvertError(ex, value, targetType);
            }
        }
    }
}
