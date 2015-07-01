using System.Web.Http;
using AlexanderPo.Core;
using AlexanderPo.Filters;
using Newtonsoft.Json.Serialization;
using Owin;

namespace AlexanderPo
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Index",
                routeTemplate: "",
                defaults: new { controller = "App", action = "GetIndex" }
            );

            config.Routes.MapHttpRoute(
                name: "AppCache",
                routeTemplate: "cache.appcache",
                defaults: new { controller = "App", action = "GetAppCache" }
            );

            config.Routes.MapHttpRoute(
                name: "Content",
                routeTemplate: "{*url}",
                defaults: new { controller = "Content", action = "Get" }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            DependencyConfig.RegisterDependencies(config);

            config.Filters.Add(new ApiExceptionFilterAttribute());

            appBuilder.UseWebApi(config);
        } 

    }
}
