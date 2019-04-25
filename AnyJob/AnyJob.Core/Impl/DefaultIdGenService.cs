using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IIdGenService))]
    public class DefaultIdGenService : IIdGenService
    {
        public DefaultIdGenService(IServiceProvider provider)
        {
            
        }
        public string NewChildId(string parentId)
        {
            return Guid.NewGuid().ToString();
        }

        public string NewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
