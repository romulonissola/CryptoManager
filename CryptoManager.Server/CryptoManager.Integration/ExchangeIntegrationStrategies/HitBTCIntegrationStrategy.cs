using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.HitBTC;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class HitBTCIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private readonly IHitBTCIntegrationClient _hitBTCIntegrationClient;
        private readonly IExchangeIntegrationCache _cache;

        public ExchangesIntegratedType ExchangesIntegratedType => ExchangesIntegratedType.HitBTC;

        public HitBTCIntegrationStrategy(IExchangeIntegrationCache cache, IHitBTCIntegrationClient hitBTCIntegrationClient)
        {
            _cache = cache;
            _hitBTCIntegrationClient = hitBTCIntegrationClient;
        }

        public async Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol)
        {
            var symbol = $"{baseAssetSymbol}{quoteAssetSymbol}";
            //Workaround because HitBTC uses USDT like USD in your api 
            symbol = symbol.Replace("USDT", "USD");
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.HitBTC, symbol);
            if (price == null)
            {
                var listPrices = await _hitBTCIntegrationClient.GetTickerPricesAsync();
                price = listPrices.FirstOrDefault(a => a.Symbol.Equals(symbol));
                await _cache.AddAsync(listPrices, ExchangesIntegratedType.HitBTC, a => a.Symbol);
                if (price == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in HitBTC");
                }
            }
            return string.IsNullOrWhiteSpace(price.Last) ? decimal.Zero : decimal.Parse(price.Last);
        }
    }
}
