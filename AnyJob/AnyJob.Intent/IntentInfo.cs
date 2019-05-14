

using System;
using System.Collections.Generic;


namespace AnyJob.Intent
{

    public class InputTypeInfo
    {
        public string type { get; set; }
        public int @default { get; set; }
        public bool required { get; set; }
        public string description { get; set; }
    }

    public class OutputTypeInfo
    {
        public string type { get; set; }
    }

    public class MetaInfo
    {
        public string @ref { get; set; }
        public string description { get; set; }
        public string displayFormat { get; set; }
        public bool enabled { get; set; }
        public string kind { get; set; }
        public Dictionary<string, InputTypeInfo> inputsDesc { get; set; }
        public OutputTypeInfo outputDesc { get; set; }
    }

    public class IntentInfo
    {
        public MetaInfo meta { get; set; }
        public string action { get; set; }
        public Dictionary<string, string> inputs { get; set; }
        public string output { get; set; }
    }

}
