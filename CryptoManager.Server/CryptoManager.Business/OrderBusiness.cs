using CryptoManager.Domain.Contracts.Business;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _repository;

        public OrderBusiness(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            if(order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new InvalidOperationException("Order Item Must be Informed");
            }
            return _repository.InsertAsync(order);
        }

        public async Task<List<OrderDetailDTO>> GetOrdersDetailsByApplicationUserAsync(Guid applicationUserId)
        {
            var orders = await _repository.GetAllByApplicationUserAsync(applicationUserId);
            return orders.Select(order =>
            {
                var orderQuantity = order.OrderItems.Sum(a => a.Quantity);
                var orderPrice = order.OrderItems.Sum(a => a.Price) / order.OrderItems.Count;
                return new OrderDetailDTO()
                {
                    Id = order.Id,
                    Date = order.Date,
                    ExchangeName = order.Exchange.Name,
                    BaseAssetSymbol = order.BaseAsset.Symbol,
                    QuoteAssetSymbol = order.QuoteAsset.Symbol,
                    Quantity = orderQuantity,
                    AvgPrice = orderPrice,
                    ValuePaidWithFees = orderPrice * orderQuantity
                };
            }).ToList();
        }
    }
}
