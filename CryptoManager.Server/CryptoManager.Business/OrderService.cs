using CryptoManager.Domain.Contracts.Business;
using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManager.Business
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IExchangeIntegrationStrategyContext _exchangeIntegrationStrategyContext;

        public OrderService(IOrderRepository repository, IExchangeIntegrationStrategyContext exchangeIntegrationStrategyContext)
        {
            _repository = repository;
            _exchangeIntegrationStrategyContext = exchangeIntegrationStrategyContext;
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new InvalidOperationException("Order Item Must be Informed");
            }
            return _repository.InsertAsync(order);
        }

        public async Task<IEnumerable<OrderDetailDTO>> GetOrdersDetailsByApplicationUserAsync(Guid applicationUserId)
        {
            var orderTaskList = new List<Task<OrderDetailDTO>>();
            var orders = await _repository.GetAllByApplicationUserAsync(applicationUserId);
            orders.ForEach(order =>
            {
                orderTaskList.Add(BuildOrderDetailAsync(order));
            });
            await Task.WhenAll(orderTaskList.ToArray());
            return orderTaskList.Select(a=> a.Result);
        }

        private async Task<OrderDetailDTO> BuildOrderDetailAsync(Order order)
        {
            var orderQuantity = order.OrderItems.Sum(a => a.Quantity);
            var orderPrice = CalculateAveragePrice(order.OrderItems.ToList());
            var currentPrice = await _exchangeIntegrationStrategyContext.GetCurrentPriceAsync(
                order.BaseAsset.Symbol,
                order.QuoteAsset.Symbol,
                order.Exchange.ExchangeType);

            var valuePaidWithFees = orderPrice * orderQuantity;
            var valueSoldWithFees = currentPrice * orderQuantity;
            return new OrderDetailDTO()
            {
                Id = order.Id,
                Date = order.Date,
                ExchangeName = order.Exchange.Name,
                BaseAssetSymbol = order.BaseAsset.Symbol,
                QuoteAssetSymbol = order.QuoteAsset.Symbol,
                Quantity = orderQuantity,
                AvgPrice = orderPrice,
                ValuePaidWithFees = valuePaidWithFees,
                ValueSoldWithFees = valueSoldWithFees,
                CurrentPrice = currentPrice,
                Profit = valueSoldWithFees - valuePaidWithFees
            };
        }

        public decimal CalculateAveragePrice(List<OrderItem> orderItems)
        {
            return orderItems.Sum(order => order.Price * order.Quantity) / orderItems.Sum(order => order.Quantity);
        }
    }
}
