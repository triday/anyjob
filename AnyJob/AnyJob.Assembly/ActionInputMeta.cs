using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AnyJob.Assembly
{
    public class ActionInputMeta : IActionInputMeta
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public object DefaultValue { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSecret { get; set; }

        public string Type { get; set; }

        public PropertyInfo Property { get; set; }
    }
}
