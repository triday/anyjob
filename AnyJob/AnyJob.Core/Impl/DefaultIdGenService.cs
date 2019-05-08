using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IIdGenService))]
    public class DefaultIdGenService : IIdGenService
    {
        Random random = new Random();
        public string NewChildId(string parentId)
        {
            return Guid.NewGuid().ToString();
        }

        public string NewId()
        {
            return random.Next(10000, 100000).ToString();
        }
    }
}
