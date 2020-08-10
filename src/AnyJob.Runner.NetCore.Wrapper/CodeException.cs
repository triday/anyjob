using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace AnyJob.Runner.NetCore.Wrapper
{
    public class CodeException : Exception
    {
        public string Code { get; set; }

        public CodeException()
        {

        }

        public CodeException(string message) : base(message)
        {

        }
        public CodeException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public CodeException(string code, string message) : base(message)
        {
            this.Code = code;
        }

        public CodeException(string code, string message, Exception innerException) : base(message, innerException)
        {
            this.Code = code;
        }

        #region static

        public static CodeException LoadAssemblyError(string assemblyName, Exception exception)
        {
            return FromErrorCode(nameof(ErrorCode.E400121), exception, assemblyName);
        }
        public static CodeException LoadTypeError(string typeName, string assemblyName, Exception exception)
        {
            return FromErrorCode(nameof(ErrorCode.E400122), exception, typeName, assemblyName);
        }
        public static CodeException DuplicateMethodError(string methodName, Type type)
        {
            return FromErrorCode(nameof(ErrorCode.E400123), methodName, type.FullName);
        }
        public static CodeException MethodNotFoundError(string methodName, Type type)
        {
            return FromErrorCode(nameof(ErrorCode.E400124), methodName, type.FullName);
        }
        public static CodeException ReadInputFileError(string filePath, Exception exception)
        {
            return FromErrorCode(nameof(ErrorCode.E400125), exception, filePath);
        }
        public static CodeException ConvertParameterValueError(ParameterInfo parameterInfo, Exception exception)
        {
            return FromErrorCode(nameof(ErrorCode.E400126), exception, parameterInfo.Name);
        }

        public static CodeException InvokeMethodError(Exception exception)
        {
            return FromErrorCode(nameof(ErrorCode.E400127), exception);
        }
        private static CodeException FromErrorCode(string code, Exception exception, params object[] args)
        {
            string format = ErrorCode.ResourceManager.GetString(code, CultureInfo.InvariantCulture);
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            return new CodeException(code, message, exception);
        }
        private static CodeException FromErrorCode(string code, params object[] args)
        {
            string format = ErrorCode.ResourceManager.GetString(code, CultureInfo.InvariantCulture);
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            return new CodeException(code, message);
        }
        #endregion
    }
}
