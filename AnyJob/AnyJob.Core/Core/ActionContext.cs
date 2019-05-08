using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    public class ActionContext : IActionContext
    {
        private IValueProvider valueProvider;
        private IServiceProvider serviceProvider;
        public ActionContext(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public IActionParameters Parameters { get; set; }
        public IActionMeta MetaInfo { get; set; }
        public CancellationToken Token { get; set; }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }
    }
}
