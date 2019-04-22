using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.DI;
namespace AnyJob.Impl
{
    public class DIContainer : IServiceContainer, IInstanceContainer,IDisposable
    {
        private Dictionary<Type, object> serviceMaps = new Dictionary<Type, object>();

        public void Dispose()
        {
            this.serviceMaps.Clear();
        }

        public T GetInstance<T>()
        {
            if (serviceMaps.TryGetValue(typeof(T), out object factory))
            {
                return ((Func<IServiceProvider, T>)factory)(this);
            }
            else
            {
                return default(T);
            }
        }

        public object GetService(Type serviceType)
        {
            if (serviceMaps.TryGetValue(serviceType, out object service))
            {
                return service;
            }
            else
            {
                return null;
            }
        }

        public void RegisteInstance<T>(Func<IServiceProvider, T> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            serviceMaps[typeof(T)] = factory;
        }

        public void RegisteService<T>(T service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            serviceMaps[typeof(T)] = service;
        }

    }
}
