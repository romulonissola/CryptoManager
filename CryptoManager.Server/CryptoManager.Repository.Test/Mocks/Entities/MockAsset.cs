using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Test.Mocks.Entities
{
    public class MockAsset
    {
        public static AssetRepository GetDBTestRepository()
        {
            var entityORM = MockEntityRepository<Asset>.GetRepoTestInMemory();
            return new AssetRepository(entityORM);
        }

        public static Asset GetEntityFake()
        {
            return new Asset()
            {
                Name = "Teste",
                Description = "descrição",
                Symbol = "symbol"
            };
        }
    }
}
