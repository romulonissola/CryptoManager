using CryptoManager.WebApi.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.WebApi.Test
{
    public class TokenControllerTest
    {
        [Fact]
        public async Task Should_Return_A_Token_When_User_Is_Valid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = "/api/Token";
            var list = new List<KeyValuePair<string,string>>();
            list.Add(new KeyValuePair<string, string>("username", "teste"));
            list.Add(new KeyValuePair<string, string>("password", "teste"));
            var result = await client.PostAsync(path,list);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_Return_Error_400_When_User_Is_InValid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = "/api/Token";
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("username", "teste"));
            list.Add(new KeyValuePair<string, string>("password", "outro"));
            var result = await client.PostAsync(path, list);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
