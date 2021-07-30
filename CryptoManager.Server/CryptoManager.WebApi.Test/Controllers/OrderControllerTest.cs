using CryptoManager.Domain.DTOs;
using CryptoManager.WebApi.Test.Mocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.WebApi.Test
{
    public class OrderControllerTest
    {
        private const string ROUTE_PATH = "/api/Order";

        //[Fact]
        public async Task Should_Return_OK_When_PostAsync()
        {
            var client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            await client.AddAuthorizationAsync();
            var entity = new OrderDTO()
            {
                Date = DateTime.Now,
                ExchangeId = Guid.NewGuid(),
                BaseAssetId = Guid.NewGuid(),
                QuoteAssetId = Guid.NewGuid(),
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO()
                    {
                        Price = 1,
                        Quantity = 1,
                        Fee = 1,
                        FeeAssetId = Guid.NewGuid()
                    }
                }
            };
            var result = await client.PostAsync(ROUTE_PATH, entity);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        //[Fact]
        public async Task Should_Return_OK_When_Get_By_User_Async()
        {
            var client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            await client.AddAuthorizationAsync();
            var entity = new OrderDTO()
            {
                Date = DateTime.Now,
                ExchangeId = Guid.NewGuid(),
                BaseAssetId = Guid.NewGuid(),
                QuoteAssetId = Guid.NewGuid(),
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO()
                    {
                        Price = 1,
                        Quantity = 1,
                        Fee = 1,
                        FeeAssetId = Guid.NewGuid()
                    }
                }
            };
            var result = await client.PostAsync(ROUTE_PATH, entity);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            await client.AddAuthorizationAsync();

            result = await client.GetAsync($"{ROUTE_PATH}/GetOrderDetailsByApplicationUser");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        //[Fact]
        public async Task Should_Return_OK_When_DeleteAsync()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            await client.AddAuthorizationAsync();
            var entity = new OrderDTO()
            {
                Date = DateTime.Now,
                ExchangeId = Guid.NewGuid(),
                BaseAssetId = Guid.NewGuid(),
                QuoteAssetId = Guid.NewGuid(),
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO()
                    {
                        Price = 1,
                        Quantity = 1,
                        Fee = 1,
                        FeeAssetId = Guid.NewGuid()
                    }
                }
            };
            var result = await client.PostAsync(ROUTE_PATH, entity);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            entity = JsonConvert.DeserializeObject<OrderDTO>(await result.Content.ReadAsStringAsync());
            result = await client.DeleteAsync($"{ROUTE_PATH}/{entity.Id}");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        //[Fact]
        public async Task Should_Return_NotFound_When_Not_Exist_In_DeleteAsync()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            await client.AddAuthorizationAsync();
            var id = new Guid();
            var result = await client.DeleteAsync($"{ROUTE_PATH}/{id}");
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

    }
}
