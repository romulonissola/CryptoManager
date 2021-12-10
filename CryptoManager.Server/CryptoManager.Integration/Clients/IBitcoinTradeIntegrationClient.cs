using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges.BitcoinTrade;
using Refit;
namespace CryptoManager.Integration.Clients
{
    public interface IBitcoinTradeIntegrationClient
    {
        [Get("/v3/public/{symbol}/ticker")]
        Task<ResponseData<TickerPrice>> GetTickerPriceAsync(string symbol);
    }
}
