using System;
using System.Collections.Generic;
using AlexanderPo.Services;
using AlexanderPo.Services.Instances;

namespace AlexanderPo.Core
{
    public class ServiceManager : IServiceManager
    {
        private static readonly Dictionary<Type, Type> MapTypes = new Dictionary<Type, Type>();

        static ServiceManager()
        {
            MapTypes.Add(typeof(ISystemService), typeof(SystemService));
        }

        private readonly RepositoryManager _repositoryManager = new RepositoryManager();

        public T Get<T>()
        {
            return (T)Activator.CreateInstance(MapTypes[typeof(T)], _repositoryManager);
        }
    }
}