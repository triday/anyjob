

using System;
using System.Collections.Generic;


namespace AnyJob.Intent
{

    public class InputType
    {
        public string type { get; set; }
        public int @default { get; set; }
        public bool required { get; set; }
        public string description { get; set; }
    }

    public class OutputType
    {
        public string type { get; set; }
    }

    public class MetaInfo
    {
        public string name { get; set; }
        public string description { get; set; }
        public string displayFormat { get; set; }
        public Dictionary<string, InputType> inputsDesc { get; set; }
        public OutputType outputDesc { get; set; }
    }

    public class IntentInfo
    {
        public MetaInfo meta { get; set; }
        public string action { get; set; }
        public Dictionary<string, string> inputs { get; set; }
        public string output { get; set; }
    }

}
