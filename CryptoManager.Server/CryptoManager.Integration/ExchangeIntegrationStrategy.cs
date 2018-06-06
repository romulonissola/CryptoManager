using CryptoManager.Domain.Entities;
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
        
        public ExchangeIntegrationStrategy(Exchange exchange, ExchangeIntegrationCache cache)
        {
            switch (exchange.ExchangeType)
            {
                case ExchangesIntegratedType.Binance:
                    _strategy = new BinanceIntegrationStrategy(exchange.APIUrl, cache);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid IntegrationType, invalidType={exchange.ExchangeType.ToString()}");
            }
        }
        public Task<decimal> GetCurrentPrice(string symbol)
        {
            return _strategy.GetCurrentPrice(symbol);
        }
    }
}
