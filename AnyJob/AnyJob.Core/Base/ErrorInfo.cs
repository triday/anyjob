using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public sealed class ErrorInfo
    {
        public string Code { get; set; }
        public string Format { get; set; }

        public ActionException ToException(Exception cause, params object[] args)
        {
            string code = this.Code ?? string.Empty;
            string message = string.Format(this.Format ?? string.Empty, args);
            return new ActionException(code, message, cause);
        }
        public ActionException ToException(params object[] args)
        {
            return ToException(null, args);
        }
    }
}
