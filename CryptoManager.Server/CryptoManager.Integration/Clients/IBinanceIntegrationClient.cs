using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges.Binance;
using Refit;
namespace CryptoManager.Integration.Clients
{
    public interface IBinanceIntegrationClient
    {
        [Get("/v3/ticker/price")]
        Task<IEnumerable<TickerPrice>> GetTickerPricesAsync();
    }
}
