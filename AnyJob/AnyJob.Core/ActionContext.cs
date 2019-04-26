using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionContext :  IActionContext
    {
        private IValueProvider valueProvider;
        public ActionContext()
        {
            
        }
        public ActionParameters Parameters { get; set; }
        public ActionMeta Meta { get; set; }

      
    }
}
