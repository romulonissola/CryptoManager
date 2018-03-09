using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(IORM<Asset> orm) : base(orm)
        {

        }
    }
}
