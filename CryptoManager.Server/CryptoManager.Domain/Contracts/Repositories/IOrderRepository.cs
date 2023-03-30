using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetAllByApplicationUserAsync(
            Guid applicationUserId,
            bool isViaRoboTrader,
            OrderType orderType = OrderType.Buy);
    }
}
