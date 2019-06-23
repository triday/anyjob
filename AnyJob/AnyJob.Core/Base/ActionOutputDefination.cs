
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionOutputDefination : IActionOutputDefination
    {
        public IActionType Type { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        

        public bool IsSecret { get; set; }
    }
    
}
