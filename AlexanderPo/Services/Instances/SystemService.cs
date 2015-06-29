using System;
using System.IO;
using AlexanderPo.Models;
using AlexanderPo.Repositories;

namespace AlexanderPo.Services.Instances
{
    public class SystemService : ServiceBase, ISystemService
    {
        public SystemService(IRepositoryManager repository)
            : base(repository)
        {
        }

        public AuthorizeModel Authorize(string authKey)
        {
            var authModel = new AuthorizeModel();
            var repository = Repository.Get<IMembershipRepository>();
            
            return authModel;
        }
    }
}