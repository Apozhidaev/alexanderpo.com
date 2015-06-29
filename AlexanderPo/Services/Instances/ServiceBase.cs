using System;
using AlexanderPo.Repositories;

namespace AlexanderPo.Services.Instances
{
    public abstract class ServiceBase
    {
        private readonly IRepositoryManager _repository;

        protected ServiceBase(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IRepositoryManager Repository
        {
            get { return _repository; }
        }
    }
}