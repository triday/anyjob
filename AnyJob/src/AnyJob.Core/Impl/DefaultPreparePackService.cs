using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    public class DefaultPreparePackService : IPreparePackService
    {
        public void PreparePack(IActionName actionName, IActionRuntime actionRuntime)
        {
            string key = $"{actionName.Pack}@{actionName.Version}";
            using (Mutex mut = new Mutex(false, key, out bool createdNew))
            {
                if (createdNew)
                {
                    
                }
                else
                {

                }
            }
        }



    }
}
