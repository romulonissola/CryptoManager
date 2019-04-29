using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.BitcoinTrade;
using CryptoManager.Integration.Utils;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class BitcoinTradeIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private HttpClientFactory _httpClientFactory;
        private readonly IExchangeIntegrationCache _cache;
        public BitcoinTradeIntegrationStrategy(string apiURL, IExchangeIntegrationCache cache)
        {
            _cache = cache;
            _httpClientFactory = new HttpClientFactory(apiURL);
        }

        public async Task<decimal> GetCurrentPrice(string baseAssetSymbol, string quoteAssetSymbol)
        {
            //bitcointrade has pairs inverted use BRLBTC instead BTCBRL
            var symbol = $"{quoteAssetSymbol}{baseAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.BitcoinTrade, symbol);
            if (price == null)
            {
                var apiPath = $"/v2/public/{symbol}/ticker";
                var response = await _httpClientFactory.GetAsync<ResponseData<TickerPrice>>(apiPath);
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
