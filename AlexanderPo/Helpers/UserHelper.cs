//using System.Linq;
//using System.Net.Http;

//namespace AlexanderPo.Helpers
//{
//    public static class UserHelper
//    {
//        public static string GetToken(this HttpRequestMessage request)
//        {
//            const string tokenKey = "authToken";
//            var cookie = request.Headers.GetCookies(tokenKey).FirstOrDefault();
//            if (cookie != null)
//            {
//                return cookie[tokenKey].Value;
//            }
//            return null;
//        }
//    }
//}