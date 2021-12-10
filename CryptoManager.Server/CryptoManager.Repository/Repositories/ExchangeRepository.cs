using System.Threading.Tasks;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CryptoManager.Repository.Repositories
{
    public class ExchangeRepository : Repository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(IORM<Exchange> orm) : base(orm)
        {
        }

        public Task<Exchange> GetByExchangeTypeAsync(ExchangesIntegratedType exchangesIntegratedType)
        {
            return _ORM.GetMany(a => a.ExchangeType.Equals(exchangesIntegratedType)).FirstOrDefaultAsync();
        }
    }
}
