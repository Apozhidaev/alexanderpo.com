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
        private static readonly string Root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..\\"));
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
                scriptBuilder.Add("<script>AlPo.debug = true;</script>");
                scriptBuilder.AddScriptsFrom("..\\scripts.config");
            }
            else
            {
                scriptBuilder.AddScript("alpo.js");
            }

            const string path = "..\\index.html";
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
                scriptBuilder.AddCacheFrom("..\\scripts.config");
            }
            else
            {
                scriptBuilder.AddCache("alpo.js");
            }

            const string path = "..\\cache.appcache";
            var content = File.ReadAllText(path)
                .Replace("$Version", Version)
                .Replace("$Scripts", scriptBuilder.Build());

            var response = new HttpResponseMessage { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/cache-manifest");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Get(string url)
        {
            var path = Path.GetFullPath(Path.Combine("..\\", url));
            if (Root == path.Substring(0, Root.Length) && File.Exists(path))
            {
                var mediaType = GetMediaType(Path.GetExtension(path));
                if (!String.IsNullOrEmpty(mediaType))
                {
                    var response = new HttpResponseMessage { Content = new ByteArrayContent(File.ReadAllBytes(path)) };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                    return response;
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        private static string GetMediaType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".html":
                    return "text/html";
                case ".js":
                    return "application/javascript";
                case ".css":
                    return "text/css";
                case ".ico":
                    return "image/vnd.microsoft.icon";
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpeg";
                case ".eot":
                    return "application/vnd.ms-fontobject";
                case ".svg":
                    return "image/svg+xml";
                case ".ttf":
                    return "application/font-ttf";
                case ".woff":
                    return "application/font-woff";
                case ".woff2":
                    return "application/font-woff";
                default:
                    return null;
            }
        }
    }
}
