using System.Web.Http;

namespace AlexanderPo.Core
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies(HttpConfiguration config)
        {
            config.DependencyResolver = new WebApiDependencyResolver();
        }
    }
}