using System;
using System.Collections.Generic;
using AlexanderPo.Repositories;
using AlexanderPo.Repositories.Instances;

namespace AlexanderPo.Core
{
    public class RepositoryManager : IRepositoryManager
    {
        private static readonly Dictionary<Type, Type> MapTypes = new Dictionary<Type, Type>();

        static RepositoryManager()
        {
            MapTypes.Add(typeof(IMembershipRepository), typeof(MembershipRepository));
        }

        public T Get<T>()
        {
            return (T)Activator.CreateInstance(MapTypes[typeof(T)]);
        }
    }
}