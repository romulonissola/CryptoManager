using CryptoManager.Domain.Entities;
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
            var itemRepository = MockOrderItem.GetDBTestRepository();
            var repository = MockOrder.GetDBTestRepository(itemRepository);
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
    }
}
