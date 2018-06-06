using CryptoManager.Integration.ExchangeIntegrationStrategies;
using CryptoManager.Integration.Utils;
using Moq;
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
            var cacheMock = new Mock<ExchangeIntegrationCache>(MockBehavior.Strict);
            var strategy = new BinanceIntegrationStrategy(BINANCE_API, cacheMock.Object);
            var price = await strategy.GetCurrentPrice("LTCBTC");
            Assert.True(price > 0);
        }
    }
}
