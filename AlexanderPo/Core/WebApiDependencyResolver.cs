using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using AlexanderPo.Controllers;

namespace AlexanderPo.Core
{
    public class WebApiDependencyResolver : IDependencyResolver
    {
        public void Dispose() { }

        public object GetService(Type serviceType)
        {
            return serviceType.IsSubclassOf(typeof(ControllerBase)) ? Activator.CreateInstance(serviceType, new ServiceManager()) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}