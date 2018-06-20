using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CryptoManager.Repository.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(IORM<OrderItem> orm) : base(orm)
        {

        }

        public Task<List<OrderItem>> GetAllByOrderIdAsync(Guid orderId)
        {
            return base._ORM.GetMany(a => a.OrderId.Equals(orderId)).ToListAsync();
        }
    }
}
