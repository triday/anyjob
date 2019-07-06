using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public abstract class ErrorInfo
    {
        public ErrorInfo(string code)
        {
            this.Code = code;
        }
        public string Code { get; private set; }
        protected abstract string OnGetMessageFormat(string code);

        public ActionException ToException(Exception cause, params object[] args)
        {
            string code = this.Code ?? string.Empty;
            string format = this.OnGetMessageFormat(this.Code);
            string message = string.Format(format ?? string.Empty, args);
            return new ActionException(code, message, cause);
        }
        public ActionException ToException(params object[] args)
        {
            return ToException(null, args);
        }
    }
}
