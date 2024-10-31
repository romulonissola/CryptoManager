using System.Threading.Tasks;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.IntegrationEntities.Exchanges;

namespace CryptoManager.Domain.Contracts.Integration
{
    public interface IExchangeIntegrationStrategyContext
    {
        Task<ObjectResult<decimal>> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol, ExchangesIntegratedType exchangesIntegratedType);
        Task<SimpleObjectResult> TestIntegrationUpAsync(ExchangesIntegratedType exchangesIntegratedType);
    }
}