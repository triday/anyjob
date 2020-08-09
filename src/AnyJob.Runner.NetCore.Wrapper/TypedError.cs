using System;
using System.Collections.Generic;
using System.Linq;
namespace AnyJob.Runner.NetCore.Wrapper
{
    public class TypedError
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }
        public string Code { get; set; }
        public TypedError Inner { get; set; }
        public List<TypedError> Inners { get; set; }

        public static TypedError FromException(Exception exception, int maxDepth = 20)
        {
            if (exception == null || maxDepth == 0) return null;
            if (exception is AggregateException aggregateException)
            {
                var flatten = aggregateException.Flatten();
                if (flatten.InnerExceptions.Count == 1)
                {
                    return FromException(flatten.InnerExceptions[0], maxDepth);
                }
                else
                {
                    //mutil exceptions
                    return new TypedError()
                    {
                        Type = flatten.GetType().FullName,
                        Message = flatten.Message,
                        Stack = flatten.StackTrace,
                        Inners = flatten.InnerExceptions.Select(ex => FromException(ex, maxDepth - 1)).Where(e => e != null).ToList()
                    };
                }
            }

            if (exception is CodeException codeException)
            {
                return new TypedError()
                {
                    Code = codeException.Code,
                    Message = codeException.Message,
                    Type = codeException.GetType().FullName,
                    Stack = codeException.StackTrace,
                    Inner = FromException(codeException.InnerException, maxDepth - 1)
                };
            }
            return new TypedError()
            {
                Message = exception.Message,
                Type = exception.GetType().FullName,
                Stack = exception.StackTrace,
                Inner = FromException(exception.InnerException, maxDepth - 1)
            };
        }
    }
}
