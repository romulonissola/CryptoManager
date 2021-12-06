using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Coinbase;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.Utils;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class CoinbaseIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private ICoinbaseIntegrationClient _coinbaseIntegrationClient;
        private readonly IExchangeIntegrationCache _cache;

        public ExchangesIntegratedType ExchangesIntegratedType => ExchangesIntegratedType.Coinbase;

        public CoinbaseIntegrationStrategy(IExchangeIntegrationCache cache, ICoinbaseIntegrationClient coinbaseIntegrationClient)
        {
            _cache = cache;
            _coinbaseIntegrationClient = coinbaseIntegrationClient;
        }

        public async Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol)
        {
            var symbol = $"{baseAssetSymbol}-{quoteAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.Coinbase, symbol);
            if (price == null)
            {
                price = await _coinbaseIntegrationClient.GetTickerPriceAsync(symbol);
                if(price == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in Coinbase");
                }
                await _cache.AddAsync(price, ExchangesIntegratedType.Coinbase, symbol);
            }
            return decimal.Parse(price.Price);
        }
    }
}
