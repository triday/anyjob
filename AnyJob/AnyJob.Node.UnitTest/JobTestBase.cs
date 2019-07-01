using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AnyJob.Node
{
    public class JobTestBase
    {
        
        [TestInitialize]
        public virtual void Before()
        {
            AnyJob.Node.NodeAction nodeAction = null;
            if (ServiceCenter.CurrentProvider == null)
            {
                ServiceCenter.RegisteDomainService();
            }
        }
    }
}
