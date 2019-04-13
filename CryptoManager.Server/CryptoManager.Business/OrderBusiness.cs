using CryptoManager.Domain.Contracts.Business;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using CryptoManager.Integration;
using CryptoManager.Integration.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManager.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _repository;
        private readonly IExchangeIntegrationCache _cache;

        public OrderBusiness(IOrderRepository repository, IExchangeIntegrationCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new InvalidOperationException("Order Item Must be Informed");
            }
            return _repository.InsertAsync(order);
        }

        public async Task<List<OrderDetailDTO>> GetOrdersDetailsByApplicationUserAsync(Guid applicationUserId)
        {
            List<Task<OrderDetailDTO>> list = new List<Task<OrderDetailDTO>>();
            var orders = await _repository.GetAllByApplicationUserAsync(applicationUserId);
            orders.ForEach(order =>
            {
                list.Add(BuildOrderDetailAsync(order));
            });
            await Task.WhenAll(list.ToArray());
            return list.Select(a=> a.Result).ToList();
        }

        private async Task<OrderDetailDTO> BuildOrderDetailAsync(Order order)
        {
            var exchangeStrategy = new ExchangeIntegrationStrategy(order.Exchange, _cache);
            var orderQuantity = order.OrderItems.Sum(a => a.Quantity);
            var orderPrice = CalculateAveragePrice(order.OrderItems.ToList());
            var currentPrice = await exchangeStrategy.GetCurrentPrice(order.BaseAsset.Symbol, order.QuoteAsset.Symbol);
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
