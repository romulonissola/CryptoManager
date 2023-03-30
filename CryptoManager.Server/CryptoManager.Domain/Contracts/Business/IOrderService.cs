using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Business
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<OrderDetailDTO>> GetOrdersDetailsByApplicationUserAsync(Guid applicationUserId, bool isViaRoboTrader);
    }
}
