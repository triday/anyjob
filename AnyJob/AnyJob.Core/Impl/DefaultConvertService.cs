using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IConvertService))]
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
                throw new ActionException("Convert error.", ex);
            }
           
        }
    }
}
