using AlexanderPo.Models;

namespace AlexanderPo.Services
{
    public interface ISystemService
    {
        AuthorizeModel Authorize(string authKey);
    }
}