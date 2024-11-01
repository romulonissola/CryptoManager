﻿using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.BitcoinTrade;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.Utils;
using Polly;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CryptoManager.Integration.ExchangeIntegrationStrategies
{
    public class BitcoinTradeIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private const int _numberOfRetries = 10;
        private readonly IBitcoinTradeIntegrationClient _bitcoinTradeIntegrationClient;
        private readonly IExchangeIntegrationCache _cache;

        public ExchangesIntegratedType ExchangesIntegratedType => ExchangesIntegratedType.BitcoinTrade;

        public BitcoinTradeIntegrationStrategy(IExchangeIntegrationCache cache, IBitcoinTradeIntegrationClient bitcoinTradeIntegrationClient)
        {
            _cache = cache;
            _bitcoinTradeIntegrationClient = bitcoinTradeIntegrationClient;
        }

        public async Task<ObjectResult<decimal>> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol)
        {
            //bitcointrade has pairs inverted use BRLBTC instead BTCBRL
            var symbol = $"{quoteAssetSymbol}{baseAssetSymbol}";
            var price = await _cache.GetAsync<TickerPrice>(ExchangesIntegratedType.BitcoinTrade, symbol);
            if (price == null)
            {
                var response = await Policy
                    .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
                    .RetryAsync(_numberOfRetries)
                    .ExecuteAsync(async () => await _bitcoinTradeIntegrationClient.GetTickerPriceAsync(symbol));

                if(response.Data == null)
                {
                    return ObjectResult<decimal>.Error($"symbol {symbol} not exists in Bitcointrade");
                }
                price = response.Data;
                await _cache.AddAsync(price, ExchangesIntegratedType.BitcoinTrade, symbol);
            }
            return ObjectResult<decimal>.Success(price.Sell);
        }

        public async Task<SimpleObjectResult> TestIntegrationUpAsync()
        {
            try
            {
                var response = await _bitcoinTradeIntegrationClient.GetTickerPriceAsync("BRLBTC");
                if (response.Data == null)
                {
                    return SimpleObjectResult.Error(response.Message);
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
