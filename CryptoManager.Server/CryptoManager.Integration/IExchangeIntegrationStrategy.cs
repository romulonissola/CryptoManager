using System.Threading.Tasks;

namespace CryptoManager.Integration
{
    public interface IExchangeIntegrationStrategy
    {
        Task<decimal> GetCurrentPrice(string baseAssetSymbol, string quoteAssetSymbol);
    }
}