using AnyJob.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.UnitTest
{
    public class JobTestBase
    {

        [TestInitialize]
        public virtual void Before()
        {
            AssemblyLoader.LoadAssemblies("Anyjob.*.dll");
            if (ServiceCenter.CurrentProvider == null)
            {
                ServiceCenter.RegisteDomainService();
            }
        }
    }
}
