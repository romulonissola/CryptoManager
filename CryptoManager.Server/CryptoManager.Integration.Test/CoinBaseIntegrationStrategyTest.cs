using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Coinbase;
using CryptoManager.Integration.ExchangeIntegrationStrategies;
using CryptoManager.Integration.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Integration.Test
{
    public class CoinbaseIntegrationStrategyTest
    {
        private const string COINBASE_API = "https://api.pro.coinbase.com/";

        [Fact]
        public async Task Should_Return_Price_Async()
        {
            TickerPrice ticker = null;
            var symbol = "BTC-GBP";
            var cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            cacheMock.Setup(repo => repo.GetAsync<TickerPrice>(ExchangesIntegratedType.Coinbase, symbol))
                .ReturnsAsync(ticker);

            cacheMock.Setup(repo => repo.AddAsync(It.IsAny<TickerPrice>(), 
                                                  ExchangesIntegratedType.Coinbase, 
                                                  It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var strategy = new CoinbaseIntegrationStrategy(COINBASE_API, cacheMock.Object);
            var price = await strategy.GetCurrentPrice("BTC", "GBP");
            Assert.True(price > 0);
        }


        [Fact]
        public async Task Should_Return_Exception_When_Symbol_Not_Exists_In_Exchange_Async()
        {
            TickerPrice ticker = null;
            var symbol = "nuncatera-jsdhjkdhsajkdh";
            var cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            cacheMock.Setup(repo => repo.GetAsync<TickerPrice>(ExchangesIntegratedType.Coinbase, symbol))
                .ReturnsAsync(ticker);

            cacheMock.Setup(repo => repo.AddAsync(It.IsAny<TickerPrice>(),
                                                  ExchangesIntegratedType.Coinbase,
                                                  It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var strategy = new CoinbaseIntegrationStrategy(COINBASE_API, cacheMock.Object);
            var price = await strategy.GetCurrentPrice("nuncatera", "jsdhjkdhsajkdh");
            Assert.Equal(0, price);
        }
    }
}
