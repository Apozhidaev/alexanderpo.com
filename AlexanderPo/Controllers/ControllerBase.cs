using System.Web.Http;
using AlexanderPo.Services;

namespace AlexanderPo.Controllers
{
    public abstract class ControllerBase : ApiController
    {
        private readonly IServiceManager _serviceManager;

        protected ControllerBase(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        protected IServiceManager Service
        {
            get { return _serviceManager; }
        }
    }
}