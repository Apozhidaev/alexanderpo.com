using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using AlexanderPo.Helpers;
using AlexanderPo.Services;

namespace AlexanderPo.Controllers
{
    public class AppController : ControllerBase
    {
#if DEBUG
        private static readonly string Version = Guid.NewGuid().ToString();
#else
        private const string Version = "0.1.11";
#endif

        public AppController(IServiceManager serviceManager) 
            : base(serviceManager)
        {
        }

        [HttpGet]
        public HttpResponseMessage GetIndex(bool debug = false)
        {
#if DEBUG
            debug = true;
#endif
            var styleBuilder = new ContentBuilder()
                .AddStyle("less/site.css");

            var scriptBuilder = new ContentBuilder();
            if (debug)
            {
                scriptBuilder.Add("<script>__.debug = true;</script>");
                scriptBuilder.AddScriptsFrom("..\\Web\\scripts.config");
            }
            else
            {
                scriptBuilder.AddScript("app.js");
            }

            const string path = "..\\Web\\index.html";
            var content = File.ReadAllText(path)
                .Replace("$Version", Version)
                .Replace("$Styles", styleBuilder.Build())
                .Replace("$Scripts", scriptBuilder.Build())
                .Replace("$DebugParams", debug ? "?debug=true" : "");

            var response = new HttpResponseMessage { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetAppCache(bool debug = false)
        {

            var scriptBuilder = new ContentBuilder();
            if (debug)
            {
                scriptBuilder.AddCacheFrom("..\\Web\\scripts.config");
            }
            else
            {
                scriptBuilder.AddCache("app.js");
            }

            const string path = "..\\Web\\cache.appcache";
            var content = File.ReadAllText(path)
                .Replace("$Version", Version)
                .Replace("$Scripts", scriptBuilder.Build());

            var response = new HttpResponseMessage { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/cache-manifest");
            return response;
        }

    }
}
