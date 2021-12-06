using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges;

namespace CryptoManager.Domain.Contracts.Integration
{
    public interface IExchangeIntegrationStrategyContext
    {
        Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol, ExchangesIntegratedType exchangesIntegratedType);
    }
}