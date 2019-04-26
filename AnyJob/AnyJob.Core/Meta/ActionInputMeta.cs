using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    public class ActionInputMeta
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public object DefaultValue { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSecret { get; set; }

        public Type Type { get; set; }

        public string TypeSchame { get; set; }
    }
}
