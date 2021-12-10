using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Domain.IntegrationEntities.Exchanges.BitcoinTrade;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.ExchangeIntegrationStrategies;
using CryptoManager.Integration.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Integration.Test
{
    public class BitcoinTradeIntegrationStrategyTest
    {
        private const string API = "https://api.bitcointrade.com.br/";

        [Fact]
        public async Task Should_Return_Price_Async()
        {
            TickerPrice ticker = null;
            var symbol = "BRLBTC";
            var cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            cacheMock.Setup(repo => repo.GetAsync<TickerPrice>(ExchangesIntegratedType.BitcoinTrade, symbol))
                .ReturnsAsync(ticker);

            cacheMock.Setup(repo => repo.AddAsync(It.IsAny<TickerPrice>(), 
                                                  ExchangesIntegratedType.BitcoinTrade, 
                                                  It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var clientMock = new Mock<IBitcoinTradeIntegrationClient>(MockBehavior.Strict);
            clientMock.Setup(c => c.GetTickerPriceAsync(symbol))
                .ReturnsAsync(new ResponseData<TickerPrice> { Code = "200", Data = new TickerPrice { Sell = 1 } });

            var strategy = new BitcoinTradeIntegrationStrategy(cacheMock.Object, clientMock.Object);
            var price = await strategy.GetCurrentPriceAsync("BTC", "BRL");
            Assert.True(price > 0);
        }


        [Fact]
        public async Task Should_Return_Exception_When_Symbol_Not_Exists_In_Exchange_Async()
        {
            TickerPrice ticker = null;
            var symbol = "jsdhjkdhsajkdhnuncatera";
            var cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            cacheMock.Setup(repo => repo.GetAsync<TickerPrice>(ExchangesIntegratedType.BitcoinTrade, symbol))
                .ReturnsAsync(ticker);

            cacheMock.Setup(repo => repo.AddAsync(It.IsAny<TickerPrice>(),
                                                  ExchangesIntegratedType.BitcoinTrade,
                                                  It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var clientMock = new Mock<IBitcoinTradeIntegrationClient>(MockBehavior.Strict);
            clientMock.Setup(c => c.GetTickerPriceAsync(symbol))
                .ReturnsAsync(new ResponseData<TickerPrice> { Code = "200", Data = null });

            var strategy = new BitcoinTradeIntegrationStrategy(cacheMock.Object, clientMock.Object);
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await strategy.GetCurrentPriceAsync("nuncatera", "jsdhjkdhsajkdh"));
            Assert.Equal($"symbol {symbol} not exists in Bitcointrade", ex.Message);
        }
    }
}
