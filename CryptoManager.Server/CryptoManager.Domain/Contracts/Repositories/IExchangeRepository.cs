using CryptoManager.Domain.Entities;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Repositories
{
    public interface IExchangeRepository : IRepository<Exchange>
    {
        Task<Exchange> GetByExchangeTypeAsync(ExchangesIntegratedType exchangesIntegratedType);
    }
}
