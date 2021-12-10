using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Binance;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.Utils;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class BinanceIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private readonly IBinanceIntegrationClient _binanceIntegrationClient;
        private readonly IExchangeIntegrationCache _cache;

        public ExchangesIntegratedType ExchangesIntegratedType => ExchangesIntegratedType.Binance;

        public BinanceIntegrationStrategy(IExchangeIntegrationCache cache, IBinanceIntegrationClient binanceIntegrationClient)
        {
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
            _binanceIntegrationClient = binanceIntegrationClient ??
                throw new ArgumentNullException(nameof(binanceIntegrationClient));
        }

        public async Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol)
        {
            var symbol = $"{baseAssetSymbol}{quoteAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.Binance, symbol);
            if (price == null)
            {
                var listPrices = await _binanceIntegrationClient.GetTickerPricesAsync();
                price = listPrices.FirstOrDefault(a => a.Symbol.Equals(symbol));
                await _cache.AddAsync(listPrices, ExchangesIntegratedType.Binance, a => a.Symbol);
                if(price == null)
                {
                    throw new InvalidOperationException($"symbol {symbol} not exists in Binance");
                }
            }
            return decimal.Parse(price.Price);
        }
    }
}
