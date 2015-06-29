using System;
using System.Threading;
using AlexanderPo.Configuration;

namespace AlexanderPo.Repositories.Instances
{
    public class MembershipRepository : IMembershipRepository
    {
        //private static readonly ReaderWriterLockSlim _fileLoker = new ReaderWriterLockSlim();

        //public Membership Get()
        //{
        //    try
        //    {
        //        _fileLoker.EnterReadLock();
        //        return JsonHelper.Deserialize<Membership>(AppConfig.MembershipFile);
        //    }
        //    finally
        //    {
        //        _fileLoker.ExitReadLock();
        //    }
        //}

        //public void Update(Action<Membership> update)
        //{
        //    try
        //    {
        //        _fileLoker.EnterWriteLock();
        //        var membership = JsonHelper.Deserialize<Membership>(AppConfig.MembershipFile);
        //        update(membership);
        //        JsonHelper.Serialize(AppConfig.MembershipFile, membership);
        //    }
        //    finally
        //    {
        //        _fileLoker.ExitWriteLock();
        //    }
        //}
    }
}