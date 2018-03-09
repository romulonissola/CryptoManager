using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Test.Mocks.Entities
{
    public class MockExchange
    {
        public static ExchangeRepository GetDBTestRepository()
        {
            var entityORM = MockEntityRepository<Exchange>.GetRepoTestInMemory();
            return new ExchangeRepository(entityORM);
        }

        public static Exchange GetEntityFake()
        {
            return new Exchange()
            {
                Name = "Teste",
                APIUrl = "apiurl",
                Website = "website"
            };
        }
    }
}
