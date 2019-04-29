using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyJob.Assembly.Meta
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ActionAttribute : Attribute
    {
        readonly string refName;

        public ActionAttribute(string refName)
        {
            this.refName = refName;
        }

        public string RefName
        {
            get { return this.refName; }
        }
        public string Description { get; set; }

        public string DisplayFormat { get; set; }

       
    }
}
