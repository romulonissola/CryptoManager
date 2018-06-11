using CryptoManager.Domain.IntegrationEntities.Exchanges;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Integration.Utils
{
    public class ExchangeIntegrationCache : IExchangeIntegrationCache
    {
        private readonly DistributedCacheEntryOptions _cacheoptions;
        private readonly IDistributedCache _cache;
        public ExchangeIntegrationCache(IDistributedCache cache)
        {
            _cache = cache;
            _cacheoptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1));
        }

        public async Task AddAsync<T>(T entity, ExchangesIntegratedType exchangeType, string symbol) where T : class
        {
            string key = GenerateKey(exchangeType, symbol);
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(entity), _cacheoptions);
        }

        public async Task AddAsync<T>(IEnumerable<T> list, ExchangesIntegratedType exchangeType, Func<T, string> symbolSelector) where T : class
        {
            foreach (T item in list)
            {
                await AddAsync(item, exchangeType, symbolSelector(item));
            }
        }

        public async Task<T> GetAsync<T>(ExchangesIntegratedType exchangeType, string symbol) where T : class
        {
            string key = GenerateKey(exchangeType, symbol);
            var value = await _cache.GetStringAsync(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        private string GenerateKey(ExchangesIntegratedType exchangeType, string symbol)
        {
            return $"{exchangeType}/{symbol}";
        }
    }
}
