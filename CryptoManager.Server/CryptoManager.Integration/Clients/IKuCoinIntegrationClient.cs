using System.Threading.Tasks;
using CryptoManager.Domain.IntegrationEntities.Exchanges.KuCoin;
using Refit;
namespace CryptoManager.Integration.Clients
{
    public interface IKuCoinIntegrationClient
    {
        [Get("/v1/market/orderbook/level1?symbol={symbol}")]
        Task<ResponseData<TickerPrice>> GetTickerPriceAsync(string symbol);
    }
}
