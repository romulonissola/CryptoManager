using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Business
{
    public interface IOrderBusiness
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<List<OrderDetailDTO>> GetOrdersDetailsByApplicationUserAsync(Guid applicationUserId);
    }
}
