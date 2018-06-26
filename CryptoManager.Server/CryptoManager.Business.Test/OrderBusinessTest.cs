using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using Binance = CryptoManager.Domain.IntegrationEntities.Exchanges.Binance;
using HitBTC = CryptoManager.Domain.IntegrationEntities.Exchanges.HitBTC;
using CryptoManager.Integration.Utils;
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
        private Mock<IExchangeIntegrationCache> _cacheMock;

        public OrderBusinessTest()
        {
            _repositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);


            _cacheMock = new Mock<IExchangeIntegrationCache>(MockBehavior.Strict);
            _cacheMock.Setup(repo => repo.GetAsync<Binance.TickerPrice>(It.IsAny<ExchangesIntegratedType>(), It.IsAny<string>()))
                .ReturnsAsync((Binance.TickerPrice)null);

            _cacheMock.Setup(repo => repo.GetAsync<HitBTC.TickerPrice>(It.IsAny<ExchangesIntegratedType>(), It.IsAny<string>()))
                .ReturnsAsync((HitBTC.TickerPrice)null);

            _cacheMock.Setup(repo => repo.AddAsync(It.IsAny<IEnumerable<Binance.TickerPrice>>(), It.IsAny<ExchangesIntegratedType>(), It.IsAny<Func<Binance.TickerPrice, string>>()))
                .Returns(Task.CompletedTask);

            _cacheMock.Setup(repo => repo.AddAsync(It.IsAny<IEnumerable<HitBTC.TickerPrice>>(), It.IsAny<ExchangesIntegratedType>(), It.IsAny<Func<HitBTC.TickerPrice, string>>()))
                .Returns(Task.CompletedTask);

            _orderBusiness = new OrderBusiness(_repositoryMock.Object, _cacheMock.Object);
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

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderBusiness.CreateOrderAsync(order));
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

            await _orderBusiness.CreateOrderAsync(order);

            _repositoryMock.Verify(repo => repo.InsertAsync(order), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Orders_Details_By_User_Logged_Async()
        {
            var applicationUserId = Guid.NewGuid();
            var orderList = new List<Order>()
            {
                new Order()
                {
                    ApplicationUserId = applicationUserId,
                    BaseAsset = new Asset()
                    {
                        Symbol = "LTC"
                    },
                    BaseAssetId = Guid.NewGuid(),
                    Exchange = new Exchange()
                    {
                        Name = "Binance",
                        APIUrl = "https://api.binance.com/api/",
                        ExchangeType = ExchangesIntegratedType.Binance
                    },
                    ExchangeId = Guid.NewGuid(),
                    QuoteAsset = new Asset()
                    {
                        Symbol = "BTC"
                    },
                    QuoteAssetId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Price = 0.0030089M,
                            Quantity = 100,
                            Fee = 1,
                            FeeAssetId = Guid.NewGuid()
                        }
                    }
                },
                new Order()
                {
                    ApplicationUserId = applicationUserId,
                    BaseAsset = new Asset()
                    {
                        Symbol = "ETH"
                    },
                    BaseAssetId = Guid.NewGuid(),
                    Exchange = new Exchange()
                    {
                        Name = "Binance",
                        APIUrl = "https://api.binance.com/api/",
                        ExchangeType = ExchangesIntegratedType.Binance
                    },
                    ExchangeId = Guid.NewGuid(),
                    QuoteAsset = new Asset()
                    {
                        Symbol = "BTC"
                    },
                    QuoteAssetId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Price = 0.07998M,
                            Quantity = 5,
                            Fee = 1,
                            FeeAssetId = Guid.NewGuid()
                        }
                    }
                }
            };

            _repositoryMock.Setup(repo => repo.GetAllByApplicationUserAsync(applicationUserId))
                .ReturnsAsync(orderList);

            var ordersDetails = await _orderBusiness.GetOrdersDetailsByApplicationUserAsync(applicationUserId);

            _repositoryMock.Verify(repo => repo.GetAllByApplicationUserAsync(applicationUserId), Times.Once);
            Assert.IsType<List<OrderDetailDTO>>(ordersDetails);
            Assert.Equal(2, ordersDetails.Count);
        }

        [Fact]
        public void Should_Calculate_Average_Price()
        {
            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Price = 1,
                    Quantity = 1000
                },
                new OrderItem()
                {
                    Price = 2,
                    Quantity = 2000
                },
                new OrderItem()
                {
                    Price = 3,
                    Quantity = 1000
                },
                new OrderItem()
                {
                    Price = 4,
                    Quantity = 1000
                }
            };
            var result = _orderBusiness.CalculateAveragePrice(orderItems);
            Assert.Equal(2.4M, result);
        }
    }
}