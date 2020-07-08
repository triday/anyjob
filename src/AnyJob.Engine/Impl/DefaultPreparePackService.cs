using System.Threading;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
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
