using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Process
{
    public static class ErrorFactory
    {
        public static ActionException FromCode(string code, Exception exception, params object[] args)
        {
            string fmt = Errors.ResourceManager.GetString(code);
            string message = string.Format(fmt, args);
            return new ActionException(code, message, exception);
        }
        public static ActionException FromCode(string code, params object[] args)
        {
            return FromCode(code, null, args);
        }
    }
}
