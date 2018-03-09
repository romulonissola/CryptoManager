using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Repositories
{
    public class ExchangeRepository : Repository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(IORM<Exchange> orm) : base(orm)
        {
        }
    }
}
