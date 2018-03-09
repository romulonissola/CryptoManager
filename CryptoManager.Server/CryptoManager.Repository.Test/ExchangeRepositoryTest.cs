using CryptoManager.Repository.Test.Mocks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.Repository.Test
{
    public class ExchangeRepositoryTest
    {
        [Fact]
        public async Task Should_Insert_And_Get_Async()
        {
            var repository = MockExchange.GetDBTestRepository();
            var result = await repository.InsertAsync(MockExchange.GetEntityFake());
            Assert.NotNull(result);
            result = await repository.GetAsync(result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Update_Async()
        {
            var repository = MockExchange.GetDBTestRepository();
            var entity = MockExchange.GetEntityFake();
            entity.APIUrl = $"TESTE{Guid.NewGuid()}";
            var result = await repository.InsertAsync(entity);
            Assert.NotNull(result);
            result.APIUrl = "Teste Alteração";
            await repository.UpdateAsync(result);
            result = await repository.GetAsync(result.Id);
            Assert.Equal("Teste Alteração", result.APIUrl);
        }

        [Fact]
        public async Task Should_Delete_Async()
        {
            var repository = MockExchange.GetDBTestRepository();
            var result = await repository.InsertAsync(MockExchange.GetEntityFake());
            Assert.NotNull(result);
            Assert.True(!result.IsExcluded);
            await repository.DeleteAsync(result);
            result = await repository.GetAsync(result.Id);
            Assert.True(result.IsExcluded);
        }

        [Fact]
        public async Task Should_Return_All_Async()
        {
            var repository = MockExchange.GetDBTestRepository();
            await repository.InsertAsync(MockExchange.GetEntityFake());
            await repository.InsertAsync(MockExchange.GetEntityFake());
            var result = await repository.GetAll();
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async Task Should_Return_All_With_Disabled_Async()
        {
            var repository = MockExchange.GetDBTestRepository();
            await repository.InsertAsync(MockExchange.GetEntityFake());
            var disable = await repository.InsertAsync(MockExchange.GetEntityFake());
            disable.IsEnabled = false;
            await repository.UpdateAsync(disable);
            var result = await repository.GetAll();
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async Task Should_Return_All_Without_Disabled_Async()
        {
            var repository = MockExchange.GetDBTestRepository();
            await repository.InsertAsync(MockExchange.GetEntityFake());
            var disable = await repository.InsertAsync(MockExchange.GetEntityFake());
            disable.IsEnabled = false;
            await repository.UpdateAsync(disable);
            var result = await repository.GetAllWithoutDisable();
            Assert.Single(result.ToList());
        }
    }
}
