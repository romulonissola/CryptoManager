using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.HitBTC;
using CryptoManager.Integration.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class HitBTCIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private HttpClientFactory _httpClientFactory;
        private readonly IExchangeIntegrationCache _cache;
        public HitBTCIntegrationStrategy(string apiURL, IExchangeIntegrationCache cache)
        {
            _cache = cache;
            _httpClientFactory = new HttpClientFactory(apiURL);
        }

        public async Task<decimal> GetCurrentPrice(string symbol)
        {
            //Workaround because HitBTC uses USDT like USD in your api 
            symbol = symbol.Replace("USDT", "USD");
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.HitBTC, symbol);
            if (price == null)
            {
                var apiPath = "2/public/ticker";
                var listPrices = await _httpClientFactory.GetAsync<List<TickerPrice>>(apiPath);
                price = listPrices.Find(a => a.Symbol.Equals(symbol));
                await _cache.AddAsync(listPrices, ExchangesIntegratedType.HitBTC, a => a.Symbol);
                if (price == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in HitBTC");
                }
            }
            return price.Last;
        }
    }
}
