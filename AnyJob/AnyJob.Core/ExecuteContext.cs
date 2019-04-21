using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ExecuteContext:IServiceProvider
    {
        private IServiceProvider serviceProvider;
        public ExecuteContext(IServiceProvider provider)
        {
            this.serviceProvider = provider;
        }
        public string ExecuteId { get; set; }

        public string ParentId { get; set; }

        public string ActionRef { get; set; }

        public ActionEntry ActionEntry { get; set; }

        public T GetInstance<T>()
        {
            throw new NotImplementedException();
        }
    }
}
