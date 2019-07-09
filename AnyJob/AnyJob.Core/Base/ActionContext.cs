using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class ActionContext : IActionContext
    {
        public IActionParameter Parameters { get; set; }
        public IActionMeta MetaInfo { get; set; }
        public CancellationToken Token { get; set; }
        public IExecutePath ExecutePath { get;set;}
        public string ExecuteName { get; set; }
        public IActionRuntime RuntimeInfo { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public IActionLogger Logger { get; set; }
    }
}
