using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.KuCoin;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.Utils;
using System;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class KuCoinIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private readonly IKuCoinIntegrationClient _kuCoinIntegrationClient;
        private readonly IExchangeIntegrationCache _cache;

        public ExchangesIntegratedType ExchangesIntegratedType => ExchangesIntegratedType.KuCoin;

        public KuCoinIntegrationStrategy(IExchangeIntegrationCache cache, IKuCoinIntegrationClient kuCoinIntegrationClient)
        {
            _cache = cache;
            _kuCoinIntegrationClient = kuCoinIntegrationClient;
        }

        public async Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol)
        {
            var symbol = $"{baseAssetSymbol}-{quoteAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.KuCoin, symbol);
            if (price == null)
            {
                var response = await _kuCoinIntegrationClient.GetTickerPriceAsync(symbol);
                if(response.Data == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in KuCoin");
                }
                price = response.Data;
                await _cache.AddAsync(price, ExchangesIntegratedType.KuCoin, symbol);
            }
            return decimal.Parse(price.Price);
        }

        public async Task<SimpleObjectResult> TestIntegrationUpAsync()
        {
            try
            {
                var response = await _kuCoinIntegrationClient.GetTickerPriceAsync("BTC-USDT");
                if (response.Data == null)
                {
                    return SimpleObjectResult.Error($"response code: {response.Code}");
                }
                return SimpleObjectResult.Success();
            }
            catch (Exception ex)
            {
                return SimpleObjectResult.Error(ex.Message);
            }
        }
    }
}
