using System;
using System.Threading;

namespace AnyJob
{
    public class ActionContext : IActionContext
    {
        public IActionParameter Parameters { get; set; }
        public IActionMeta MetaInfo { get; set; }
        public CancellationToken Token { get; set; }
        public IExecutePath ExecutePath { get; set; }
        public string ExecuteName { get; set; }
        public IActionRuntime RuntimeInfo { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public IActionLogger Output { get; set; }
        public IActionLogger Error { get; set; }
        public IActionName Name { get; set; }
    }
}
