﻿using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Binance;
using CryptoManager.Integration.ExchangeIntegrationStrategies;
using CryptoManager.Integration.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Integration.Test
{
    public class BinanceIntegrationStrategyTest
    {
        private const string BINANCE_API = "https://api.binance.com/api/";

        [Fact]
        public async Task Should_Return_Price_Async()
        {
            TickerPrice ticker = null;
            var symbol = "LTCBTC";
            var cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            cacheMock.Setup(repo => repo.GetAsync<TickerPrice>(ExchangesIntegratedType.Binance, symbol))
                .ReturnsAsync(ticker);

            cacheMock.Setup(repo => repo.AddAsync(It.IsAny<List<TickerPrice>>(), 
                                                  ExchangesIntegratedType.Binance, 
                                                  It.IsAny<Func<TickerPrice, string>>()))
                .Returns(Task.CompletedTask);

            var strategy = new BinanceIntegrationStrategy(BINANCE_API, cacheMock.Object);
            var price = await strategy.GetCurrentPrice("LTC","BTC");
            Assert.True(price > 0);
        }


        [Fact]
        public async Task Should_Return_Exception_When_Symbol_Not_Exists_In_Exchange_Async()
        {
            TickerPrice ticker = null;
            var symbol = "nuncaterajsdhjkdhsajkdh";
            var cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            cacheMock.Setup(repo => repo.GetAsync<TickerPrice>(ExchangesIntegratedType.Binance, symbol))
                .ReturnsAsync(ticker);

            cacheMock.Setup(repo => repo.AddAsync(It.IsAny<List<TickerPrice>>(),
                                                  ExchangesIntegratedType.Binance,
                                                  It.IsAny<Func<TickerPrice, string>>()))
                .Returns(Task.CompletedTask);

            var strategy = new BinanceIntegrationStrategy(BINANCE_API, cacheMock.Object);
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await strategy.GetCurrentPrice("nuncatera", "jsdhjkdhsajkdh"));
            Assert.Equal($"symbol {symbol} not exists in Binance", ex.Message);
        }
    }
}
