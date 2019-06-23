
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionInputDefination: IActionInputDefination
    {

        public string Description { get; set; }

        public object Default { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSecret { get; set; }

        public IActionType Type { get; set; }
    }
}
