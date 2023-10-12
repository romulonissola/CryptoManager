using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetAllByApplicationUserAsync(
            Guid applicationUserId,
            bool isViaRoboTrader,
            string setupTraderId,
            DateTime? startDate = null,
            DateTime? endDate = null,
            OrderType orderType = OrderType.Buy);
    }
}
