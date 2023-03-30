using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Test.Mocks;
using CryptoManager.Repository.Test.Mocks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Repository.Test
{
    public class OrderRepositoryTest
    {
        [Fact]
        public async Task Should_Insert_And_GetOrder_And_GetOrderItem_Async()
        {
            var context = MockDbContext.CreateDBInMemoryContext();
            var itemRepository = MockOrderItem.GetDBTestRepository(context);
            var repository = MockOrder.GetDBTestRepository(context);
            var order = MockOrder.GetEntityFake();
            var orderItem = MockOrderItem.GetEntityFake();
            order.OrderItems = new List<OrderItem>
            {
                orderItem
            };
            var result = await repository.InsertAsync(order);
            Assert.NotNull(result);
            result = await repository.GetAsync(result.Id);
            Assert.NotNull(result);
            var orderItems = await itemRepository.GetAll();
            Assert.Single(orderItems);
        }

        [Fact]
        public async Task Should_Update_Async()
        {
            var repository = MockOrder.GetDBTestRepository();
            var entity = MockOrder.GetEntityFake();
            entity.Date = DateTime.Now;
            var result = await repository.InsertAsync(entity);
            Assert.NotNull(result);
            result.Date = new DateTime(2000, 1, 1);
            await repository.UpdateAsync(result);
            result = await repository.GetAsync(result.Id);
            Assert.Equal(new DateTime(2000, 1, 1), result.Date);
        }

        [Fact]
        public async Task Should_Delete_Async()
        {
            var repository = MockOrder.GetDBTestRepository();
            var result = await repository.InsertAsync(MockOrder.GetEntityFake());
            Assert.NotNull(result);
            Assert.True(!result.IsExcluded);
            await repository.DeleteAsync(result);
            result = await repository.GetAsync(result.Id);
            Assert.True(result.IsExcluded);
        }

        [Fact]
        public async Task Should_Delete_With_OrderItens_Async()
        {
            var context = MockDbContext.CreateDBInMemoryContext();
            var applicationUserId = Guid.NewGuid();
            var repository = MockOrder.GetDBTestRepository(context);
            var orderItemRepository = MockOrderItem.GetDBTestRepository(context);
            var order = MockOrder.GetEntityFake();
            order.ApplicationUserId = applicationUserId;
            order.OrderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Quantity = 100,
                    Price = 1,
                    Fee = 10,
                    FeeAsset = new Asset()
                }
            };
            order = await repository.InsertAsync(order);
            Assert.NotNull(order);
            Assert.True(!order.IsExcluded);
            Assert.True(!order.OrderItems.First().IsExcluded);
            var orderItems = await orderItemRepository.GetAllByOrderIdAsync(order.Id);
            Assert.True(orderItems.Any());
            await repository.DeleteAsync(order);
            order = await repository.GetAsync(order.Id);
            Assert.True(order.IsExcluded);
            orderItems = await orderItemRepository.GetAllByOrderIdAsync(order.Id);
            Assert.True(!orderItems.Any());
        }

        [Fact]
        public async Task Should_Return_All_Async()
        {
            var repository = MockOrder.GetDBTestRepository();
            await repository.InsertAsync(MockOrder.GetEntityFake());
            await repository.InsertAsync(MockOrder.GetEntityFake());
            var result = await repository.GetAll();
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async Task Should_Return_All_With_Disabled_Async()
        {
            var repository = MockOrder.GetDBTestRepository();
            await repository.InsertAsync(MockOrder.GetEntityFake());
            var disable = await repository.InsertAsync(MockOrder.GetEntityFake());
            disable.IsEnabled = false;
            await repository.UpdateAsync(disable);
            var result = await repository.GetAll();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Should_Return_All_Without_Disabled_Async()
        {
            var repository = MockOrder.GetDBTestRepository();
            await repository.InsertAsync(MockOrder.GetEntityFake());
            var disable = await repository.InsertAsync(MockOrder.GetEntityFake());
            disable.IsEnabled = false;
            await repository.UpdateAsync(disable);
            var result = await repository.GetAllWithoutDisable();
            Assert.Single(result.ToList());
        }

        [Fact]
        public async Task Should_Return_All_By_Application_User_Async()
        {
            var applicationUserId = Guid.NewGuid();
            var repository = MockOrder.GetDBTestRepository();
            var order = MockOrder.GetEntityFake();
            order.ApplicationUserId = applicationUserId;
            order.OrderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Quantity = 100,
                    Price = 1,
                    Fee = 10,
                    FeeAsset = new Asset()
                }
            };
            order.BaseAsset = new Asset();
            order.QuoteAsset = new Asset();
            order.Exchange = new Exchange();
            await repository.InsertAsync(order);
            await repository.InsertAsync(order);
            await repository.InsertAsync(MockOrder.GetEntityFake());

            var result = await repository.GetAllByApplicationUserAsync(applicationUserId, false);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Should_Load_Childs_In_Orders_By_Application_User_Async()
        {
            var context = MockDbContext.CreateDBInMemoryContext();
            var applicationUserId = Guid.NewGuid();
            var repository = MockOrder.GetDBTestRepository(context);
            var order = MockOrder.GetEntityFake();
            order.ApplicationUserId = applicationUserId;
            order.OrderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Quantity = 100,
                    Price = 1,
                    Fee = 10,
                    FeeAsset = new Asset()
                }
            };
            order.BaseAsset = new Asset();
            order.QuoteAsset = new Asset();
            order.Exchange = new Exchange();
            await repository.InsertAsync(order);
            await repository.InsertAsync(order);
            await repository.InsertAsync(MockOrder.GetEntityFake());

            var result = await repository.GetAllByApplicationUserAsync(applicationUserId, false);
            Assert.Equal(2, result.Count);
            Assert.True(result.First().OrderItems.Any());
        }
    }
}
