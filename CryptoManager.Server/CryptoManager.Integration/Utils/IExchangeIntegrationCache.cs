using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges;

namespace CryptoManager.Integration.Utils
{
    public interface IExchangeIntegrationCache
    {
        Task AddAsync<T>(IEnumerable<T> list, ExchangesIntegratedType exchangeType, Func<T, string> symbolSelector) where T : class;
        Task AddAsync<T>(T entity, ExchangesIntegratedType exchangeType, string symbol) where T : class;
        Task<T> GetAsync<T>(ExchangesIntegratedType exchangeType, string symbol) where T : class;
    }
}