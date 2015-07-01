using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using AlexanderPo.Configuration;
using AlexanderPo.Services;

namespace AlexanderPo.Controllers
{
    public class ContentController : ControllerBase
    {
        private static readonly string Root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..\\Web\\"));

        public ContentController(IServiceManager serviceManager)
            : base(serviceManager)
        {
        }

        [HttpGet]
        public HttpResponseMessage Get(string url)
        {
            var path = Path.GetFullPath(Path.Combine("..\\Web\\", url));
            if (Root == path.Substring(0, Root.Length) && File.Exists(path))
            {
                var mediaType = AppConfig.GetMediaType(Path.GetExtension(path));
                if (!String.IsNullOrEmpty(mediaType))
                {
                    var response = new HttpResponseMessage { Content = new ByteArrayContent(File.ReadAllBytes(path)) };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                    return response;
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
