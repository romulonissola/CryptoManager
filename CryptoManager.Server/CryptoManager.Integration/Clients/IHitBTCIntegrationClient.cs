using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges.HitBTC;
using Refit;
namespace CryptoManager.Integration.Clients
{
    public interface IHitBTCIntegrationClient
    {
        [Get("/2/public/ticker")]
        Task<IEnumerable<TickerPrice>> GetTickerPricesAsync();
    }
}
