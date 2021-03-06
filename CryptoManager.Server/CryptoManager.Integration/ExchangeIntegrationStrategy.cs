﻿using CryptoManager.Domain.Entities;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Integration.ExchangeIntegrationStrategies;
using CryptoManager.Integration.Utils;
using System;
using System.Threading.Tasks;

namespace CryptoManager.Integration
{
    public class ExchangeIntegrationStrategy : IExchangeIntegrationStrategy
    {
        private readonly IExchangeIntegrationStrategy _strategy;
        
        public ExchangeIntegrationStrategy(Exchange exchange, IExchangeIntegrationCache cache)
        {
            switch (exchange.ExchangeType)
            {
                case ExchangesIntegratedType.Binance:
                    _strategy = new BinanceIntegrationStrategy(exchange.APIUrl, cache);
                    break;
                case ExchangesIntegratedType.HitBTC:
                    _strategy = new HitBTCIntegrationStrategy(exchange.APIUrl, cache);
                    break;
                case ExchangesIntegratedType.Coinbase:
                    _strategy = new CoinbaseIntegrationStrategy(exchange.APIUrl, cache);
                    break;
                case ExchangesIntegratedType.BitcoinTrade:
                    _strategy = new BitcoinTradeIntegrationStrategy(exchange.APIUrl, cache);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid IntegrationType, invalidType={exchange.ExchangeType.ToString()}");
            }
        }
        public Task<decimal> GetCurrentPrice(string baseAssetSymbol, string quoteAssetSymbol)
        {
            return _strategy.GetCurrentPrice(baseAssetSymbol, quoteAssetSymbol);
        }
    }
}
