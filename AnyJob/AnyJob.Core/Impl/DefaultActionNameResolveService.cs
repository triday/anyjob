using AnyJob.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    public class DefaultActionNameResolveService : IActionNameResolveService
    {
        private PackOption packOption;
        public DefaultActionNameResolveService(PackOption packOption)
        {
            this.packOption = packOption;
        }

        public IActionName ResolverName(string fullName)
        {
            throw new NotImplementedException();
        }
    }
}
