using CryptoManager.Repository.Test.Mocks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Repository.Test
{
    public class OrderItemRepositoryTest
    {
        [Fact]
        public async Task Should_Insert_And_Get_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            var result = await repository.InsertAsync(MockOrderItem.GetEntityFake());
            Assert.NotNull(result);
            result = await repository.GetAsync(result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Update_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            var entity = MockOrderItem.GetEntityFake();
            entity.Quantity = 500;
            var result = await repository.InsertAsync(entity);
            Assert.NotNull(result);
            result.Quantity = 400;
            await repository.UpdateAsync(result);
            result = await repository.GetAsync(result.Id);
            Assert.Equal(400, result.Quantity);
        }

        [Fact]
        public async Task Should_Delete_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            var result = await repository.InsertAsync(MockOrderItem.GetEntityFake());
            Assert.NotNull(result);
            Assert.True(!result.IsExcluded);
            await repository.DeleteAsync(result);
            result = await repository.GetAsync(result.Id);
            Assert.True(result.IsExcluded);
        }

        [Fact]
        public async Task Should_Return_All_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            await repository.InsertAsync(MockOrderItem.GetEntityFake());
            await repository.InsertAsync(MockOrderItem.GetEntityFake());
            var result = await repository.GetAll();
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async Task Should_Return_All_With_Disabled_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            await repository.InsertAsync(MockOrderItem.GetEntityFake());
            var disable = await repository.InsertAsync(MockOrderItem.GetEntityFake());
            disable.IsEnabled = false;
            await repository.UpdateAsync(disable);
            var result = await repository.GetAll();
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async Task Should_Return_All_Without_Disabled_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            await repository.InsertAsync(MockOrderItem.GetEntityFake());
            var disable = await repository.InsertAsync(MockOrderItem.GetEntityFake());
            disable.IsEnabled = false;
            await repository.UpdateAsync(disable);
            var result = await repository.GetAllWithoutDisable();
            Assert.Single(result.ToList());
        }

        [Fact]
        public async Task Should_Return_All_by_OrderId_Async()
        {
            var repository = MockOrderItem.GetDBTestRepository();
            var item = MockOrderItem.GetEntityFake();
            await repository.InsertAsync(item);
            item.OrderId = Guid.NewGuid();
            await repository.InsertAsync(item);
            var result = await repository.GetAllByOrderIdAsync(item.OrderId);
            Assert.Single(result.ToList());
        }
    }
}
