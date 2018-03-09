using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Test.Mocks.Entities
{
    public class MockOrder
    {
        public static OrderRepository GetDBTestRepository()
        {
            var entityORM = MockEntityRepository<Order>.GetRepoTestInMemory();
            return new OrderRepository(entityORM);
        }

        public static Order GetEntityFake()
        {
            return new Order()
            {
                ApplicationUserId = Guid.NewGuid(),
                BaseAssetId = Guid.NewGuid(),
                ExchangeId = Guid.NewGuid(),
                QuoteAssetId = Guid.NewGuid(),
                Date = DateTime.Now
            };
        }
    }
}
