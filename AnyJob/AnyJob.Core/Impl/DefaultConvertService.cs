using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using AnyJob.DependencyInjection;

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
                throw ActionException.FromErrorCode(ex, nameof(ErrorCodes.CONV_ERROR));
            }
           
        }
    }
}
