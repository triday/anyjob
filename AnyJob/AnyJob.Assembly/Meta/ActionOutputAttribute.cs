using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Assembly.Meta
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ActionOutputAttribute :  Attribute
    {
        public ActionOutputAttribute(Type outputType)
        {
            this.Type = outputType;
        }
        public Type Type { get;private set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
    }
}
