using CryptoManager.Domain.Contracts.Entities;
using CryptoManager.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Test.Mocks
{
    public class MockEntityRepository<T> where T : class, IEntity
    {
        public static EntityRepository<T> GetRepoTestInMemory()
        {
            return new EntityRepository<T>(MockDbContext.CreateDBInMemoryContext());
        }
    }
}
