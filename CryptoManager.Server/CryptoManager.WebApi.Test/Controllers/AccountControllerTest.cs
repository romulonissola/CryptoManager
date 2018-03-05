using CryptoManager.Domain.Entities;
using CryptoManager.WebApi.Test.Mocks;
using CryptoManager.WebApi.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.WebApi.Test
{
    public class AccountControllerTest
    {
        [Fact]
        public async Task Should_Return_Error_400_When_AccessToken_Is_InValid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = "/api/account/ExternalLoginFacebook";
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("accessToken", ""));
            var result = await client.PostAsync(path, list);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }


        [Fact]
        public async Task Should_Return_BadRequest_When_Not_Using_Token()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());            
            string path = "/api/account";
            var result = await client.GetAsync(path);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
