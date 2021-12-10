using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges;

namespace CryptoManager.Domain.Contracts.Integration
{
    public interface IExchangeIntegrationStrategy
    {
        ExchangesIntegratedType ExchangesIntegratedType { get; }
        Task<decimal> GetCurrentPriceAsync(string baseAssetSymbol, string quoteAssetSymbol);
    }
}