using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class ActionContext : IActionContext
    {

        private IServiceProvider serviceProvider;
        public ActionContext(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public ActionParameters Parameters { get; set; }
        public IActionMeta MetaInfo { get; set; }
        public CancellationToken Token { get; set; }
        public IExecutePath ExecutePath { get;set;}
        public IActionRuntime RuntimeInfo { get; set; }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }
    }
}
