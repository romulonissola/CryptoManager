using CryptoManager.WebApi.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.WebApi.Test
{
    public class AccountControllerTest
    {
        private const string ROUTE_PATH = "/api/account";

        //[Fact]
        public async Task Should_Return_Error_400_When_AccessToken_Is_InValid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = $"{ROUTE_PATH}/ExternalLoginFacebook?accessToken=invalid";
            var result = await client.PostAsync(path);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        //[Fact]
        public async Task Should_Return_Unauthorized_When_Not_Using_Token()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());            
            var result = await client.GetAsync(ROUTE_PATH);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        //[Fact]
        public async Task Should_Return_User_Info_When_AccessToken_IsValid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            await client.AddAuthorizationAsync();
            var result = await client.GetAsync(ROUTE_PATH);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
