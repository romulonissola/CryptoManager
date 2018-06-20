using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<List<OrderItem>> GetAllByOrderIdAsync(Guid orderId);
    }
}
