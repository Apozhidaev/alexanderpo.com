//using System;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Web.Http;
//using AlexanderPo.Configuration;
//using AlexanderPo.Services;

//namespace AlexanderPo.Controllers
//{
//    public class ContentController : ControllerBase
//    {
//        public ContentController(IServiceManager serviceManager) 
//            : base(serviceManager)
//        {
//        }

//        [HttpGet]
//        public HttpResponseMessage GetAttachment(Guid userId, int issueId, string fileName)
//        {
//            var dataBytes = Service.Get<IAttachService>().GetAttachment(userId, issueId, fileName);
//            if (dataBytes != null)
//            {
//                var mediaType = AppConfig.GetMediaType(Path.GetExtension(fileName));
//                var response = new HttpResponseMessage { Content = new ByteArrayContent(dataBytes) };
//                response.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
//                return response;
//            }
//            return Request.CreateResponse(HttpStatusCode.NotFound);
//        }
//    }
//}
