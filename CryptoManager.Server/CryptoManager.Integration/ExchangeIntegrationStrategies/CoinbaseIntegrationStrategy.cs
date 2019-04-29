using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Coinbase;
using CryptoManager.Integration.Utils;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class CoinbaseIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private HttpClientFactory _httpClientFactory;
        private readonly IExchangeIntegrationCache _cache;
        public CoinbaseIntegrationStrategy(string apiURL, IExchangeIntegrationCache cache)
        {
            _cache = cache;
            _httpClientFactory = new HttpClientFactory(apiURL);
            _httpClientFactory.AddHeader("User-Agent", "gdax-node-client");
        }

        public async Task<decimal> GetCurrentPrice(string baseAssetSymbol, string quoteAssetSymbol)
        {
            var symbol = $"{baseAssetSymbol}-{quoteAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.Coinbase, symbol);
            if (price == null)
            {
                var apiPath = $"products/{symbol}/ticker";
                price = await _httpClientFactory.GetAsync<TickerPrice>(apiPath);
                if(price == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in Coinbase");
                }
                await _cache.AddAsync(price, ExchangesIntegratedType.Coinbase, symbol);
            }
            return price.Price;
        }
    }
}
