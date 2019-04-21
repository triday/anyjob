using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionEntry
    {
        public IAction Action { get; set; }
        public ActionMeta Meta { get; set; }
        public ActionParameters Parameters { get; set; }
        
    }
}
