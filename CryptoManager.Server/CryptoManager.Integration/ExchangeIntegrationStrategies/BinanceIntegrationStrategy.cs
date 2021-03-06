﻿using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Binance;
using CryptoManager.Integration.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class BinanceIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private HttpClientFactory _httpClientFactory;
        private readonly IExchangeIntegrationCache _cache;
        public BinanceIntegrationStrategy(string apiURL, IExchangeIntegrationCache cache)
        {
            _cache = cache;
            _httpClientFactory = new HttpClientFactory(apiURL);
        }

        public async Task<decimal> GetCurrentPrice(string baseAssetSymbol, string quoteAssetSymbol)
        {
            var symbol = $"{baseAssetSymbol}{quoteAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.Binance, symbol);
            if (price == null)
            {
                var apiPath = "v3/ticker/price";
                var listPrices = await _httpClientFactory.GetAsync<List<TickerPrice>>(apiPath);
                price = listPrices.Find(a => a.Symbol.Equals(symbol));
                await _cache.AddAsync(listPrices, ExchangesIntegratedType.Binance, a => a.Symbol);
                if(price == null)
                {
                    throw new System.InvalidOperationException($"symbol {symbol} not exists in Binance");
                }
            }
            return price.Price;
        }
    }
}
