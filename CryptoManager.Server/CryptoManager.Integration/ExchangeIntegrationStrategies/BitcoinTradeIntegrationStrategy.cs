using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.BitcoinTrade;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.Utils;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class BitcoinTradeIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private readonly IBitcoinTradeIntegrationClient _bitcoinTradeIntegrationClient;
        private readonly IExchangeIntegrationCache _cache;

        public ExchangesIntegratedType ExchangesIntegratedType => ExchangesIntegratedType.BitcoinTrade;

        public BitcoinTradeIntegrationStrategy(IExchangeIntegrationCache cache, IBitcoinTradeIntegrationClient bitcoinTradeIntegrationClient)
        {
            _cache = cache;
            _bitcoinTradeIntegrationClient = bitcoinTradeIntegrationClient;
        }

        public async Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol)
        {
            //bitcointrade has pairs inverted use BRLBTC instead BTCBRL
            var symbol = $"{quoteAssetSymbol}{baseAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.BitcoinTrade, symbol);
            if (price == null)
            {
                var response = await _bitcoinTradeIntegrationClient.GetTickerPriceAsync(symbol);
                if(response.Data == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in Bitcointrade");
                }
                price = response.Data;
                await _cache.AddAsync(price, ExchangesIntegratedType.BitcoinTrade, symbol);
            }
            return price.Sell;
        }
    }
}
