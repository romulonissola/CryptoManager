using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Coinbase;
using Refit;
namespace CryptoManager.Integration.Clients
{
    public interface ICoinbaseIntegrationClient
    {
        [Headers("User-Agent: gdax-node-client")]
        [Get("/products/{symbol}/ticker")]
        Task<TickerPrice> GetTickerPriceAsync(string symbol);
    }
}
