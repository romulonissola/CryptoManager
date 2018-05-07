using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Business.Test
{
    public class OrderBusinessTest
    {
        private Mock<IOrderRepository> _repositoryMock;
        private OrderBusiness _orderBusiness;

        public OrderBusinessTest()
        {
            _repositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            _orderBusiness = new OrderBusiness(_repositoryMock.Object);
        }

        [Fact]
        public async Task Should_Return_Error_When_OrderItem_Is_Empty_Async()
        {
            var order = new Order()
            {
                ApplicationUserId = Guid.NewGuid(),
                BaseAssetId = Guid.NewGuid(),
                ExchangeId = Guid.NewGuid(),
                QuoteAssetId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            _repositoryMock.Setup(repo => repo.InsertAsync(order))
                .ReturnsAsync(order);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderBusiness.CreateOrder(order));
            Assert.Equal("Order Item Must be Informed", ex.Message);
            _repositoryMock.Verify(repo => repo.InsertAsync(order), Times.Never);
        }

        [Fact]
        public async Task Should_Return_Object_When_OrderCreate_Async()
        {
            var order = new Order()
            {
                ApplicationUserId = Guid.NewGuid(),
                BaseAssetId = Guid.NewGuid(),
                ExchangeId = Guid.NewGuid(),
                QuoteAssetId = Guid.NewGuid(),
                Date = DateTime.Now,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Price = 1,
                        Quantity = 1,
                        Fee = 1,
                        FeeAssetId = Guid.NewGuid()
                    }
                }
            };

            _repositoryMock.Setup(repo => repo.InsertAsync(order))
                .ReturnsAsync(order);

            await _orderBusiness.CreateOrder(order);
            
            _repositoryMock.Verify(repo => repo.InsertAsync(order), Times.Once);
        }
    }
}