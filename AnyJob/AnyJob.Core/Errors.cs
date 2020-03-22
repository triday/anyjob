using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    static class Errors
    {
        public static ActionException ConvertError(Exception exception, object value, Type targetType)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00004), exception, ToText(value), ToText(targetType));
        }
        public static ActionException CalcExpressionValueError(Exception exception, string expression)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00005), exception, ToText(expression));
        }
        public static ActionException SerializeError(Exception exception, object value)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00006), exception, ToText(value));
        }
        public static ActionException DeserializeError(Exception exception, Type type)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00007), exception, ToText(type));
        }
        public static ActionException GetObjectError(Exception exception, string fileName, Type type)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00008), exception, ToText(fileName), ToText(type));

        }
        public static ActionException SaveObjectError(Exception exception, object value, string fileName)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00009), exception, ToText(value), ToText(fileName));
        }

       
        public static ActionException GetDynamicValueError(Exception exception)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00010), exception);
        }
        public static ActionException GetRuntimeInfoError(Exception exception, IActionName actionName)
        {
            return FromErrorMessage(nameof(ErrorCodes.E10001), exception, ToText(actionName));
        }
        public static ActionException GetMetaInfoError(Exception exception, IActionName actionName)
        {
            return FromErrorMessage(nameof(ErrorCodes.E10002), exception, ToText(actionName));
        }

        public static ActionException JobCountLimitError(int maxJobCount)
        {
            return FromErrorMessage(nameof(ErrorCodes.E10003),maxJobCount);
        }
        public static ActionException InvalidActionName(string name)
        {
            return FromErrorMessage(nameof(ErrorCodes.InvalidActionName), name);
        }
        public static ActionException ResolveNullActionName(string name)
        {
            return FromErrorMessage(nameof(ErrorCodes.E20001), name);
        }
        public static ActionException GetNullRuntimeInfo(string name)
        {
            return FromErrorMessage(nameof(ErrorCodes.E20002), name);
        }
        public static ActionException GetNullMetaInfo(string name)
        {
            return FromErrorMessage(nameof(ErrorCodes.E20003), name);
        }
        public static ActionException CannotGetActionFactory(string kind)
        {
            return FromErrorMessage(nameof(ErrorCodes.E20004), kind);
        }
        public static ActionException ActionIsDisabled(string name)
        {
            return FromErrorMessage(nameof(ErrorCodes.E20005), name);
        }

        public static ActionException InvalidJsonSchema(Type type)
        {
            return FromErrorMessage(nameof(ErrorCodes.E00011));
        }

        private static ActionException FromErrorMessage(string name, params object[] args)
        {
            string format = ErrorCodes.ResourceManager.GetString(name);
            string message = string.Format(format, args);
            return new ActionException(name, message);
        }
        private static ActionException FromErrorMessage(string name, Exception exception, params object[] args)
        {
            string format = ErrorCodes.ResourceManager.GetString(name);
            string message = string.Format(format, args);
            return new ActionException(name, message, exception);
        }
        private static string ToText(object value)
        {
            return value == null ? "[NULL]" : value.ToString();
        }
    }
}
