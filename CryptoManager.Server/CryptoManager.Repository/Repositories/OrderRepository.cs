using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoManager.Repository.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderRepository(IORM<Order> orm, IOrderItemRepository orderItemRepository) : base(orm)
        {
            _orderItemRepository = orderItemRepository;
        }

        public override async Task<Order> InsertAsync(Order entity)
        {
            var orderItens = entity.OrderItems;
            entity.OrderItems = new List<OrderItem>();
            entity = await base.InsertAsync(entity);

            if (orderItens != null)
            {
                foreach (var orderItem in orderItens)
                {
                    orderItem.OrderId = entity.Id;
                    entity.OrderItems.Add(await _orderItemRepository.InsertAsync(orderItem));
                }
            }

            return entity;
        }

        public override async Task DeleteAsync(Order entity)
        {
            var orderItems = await _orderItemRepository.GetAllByOrderIdAsync(entity.Id);
            foreach (var orderItem in orderItems)
            {
                await _orderItemRepository.DeleteAsync(orderItem);
            }
            await base.DeleteAsync(entity);
        }

        public Task<List<Order>> GetAllByApplicationUserAsync(
            Guid applicationUserId,
            bool isViaRoboTrader,
            OrderType orderType = OrderType.Buy)
        {
            return _ORM.GetManyWithoutDisable(a =>
                a.ApplicationUserId.Equals(applicationUserId) &&
                a.IsViaRoboTrader == isViaRoboTrader &&
                a.OrderType == orderType)
               .Include(order => order.BaseAsset)
               .Include(order => order.QuoteAsset)
               .Include(order => order.Exchange)
               .Include(order => order.RelatedOrder).ThenInclude(relatedOrder => relatedOrder.OrderItems)
               .Include(order => order.OrderItems).ThenInclude(orderItems => orderItems.FeeAsset)
               .ToListAsync();
        }
    }
}
