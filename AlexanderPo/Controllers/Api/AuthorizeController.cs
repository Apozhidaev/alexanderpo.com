//using System;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using AlexanderPo.Filters;
//using AlexanderPo.Services;

//namespace AlexanderPo.Controllers.Api
//{
//    [NoCache]
//    [RoutePrefix("api/authorize")]
//    public class AuthorizeController : ControllerBase
//    {
//        public AuthorizeController(IServiceManager serviceManager) 
//            : base(serviceManager)
//        {
//        }

//        [HttpGet]
//        [Route("")]
//        public async Task<HttpResponseMessage> Authorize(string authKey)
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(100));
//            return Request.CreateResponse(HttpStatusCode.OK, Service.Get<ISystemService>().Authorize(authKey));
//        }

//    }
//}
