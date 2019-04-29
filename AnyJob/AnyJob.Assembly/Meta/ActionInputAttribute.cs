using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AnyJob.Assembly.Meta
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ActionInputAttribute : Attribute
    {

        public string Description { get; set; }

        public object Default { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSecret { get; set; }

        
    }
}
