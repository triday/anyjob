using Microsoft.Extensions.DependencyInjection;
using YS.Knife;

namespace AnyJob
{
    public class ServiceRegister : IServiceRegister
    {
        public void RegisteServices(IServiceCollection services, IRegisteContext context)
        {
            services.AddHttpClient();
        }
    }
}
