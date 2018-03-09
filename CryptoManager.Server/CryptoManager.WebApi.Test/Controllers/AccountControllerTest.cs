using CryptoManager.WebApi.Test.Mocks;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CryptoManager.WebApi.Test
{
    public class AccountControllerTest
    {
        private const string ROUTE_PATH = "/api/account";

        [Fact]
        public async Task Should_Return_Error_400_When_AccessToken_Is_InValid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = $"{ROUTE_PATH}/ExternalLoginFacebook";
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", "")
            };
            var result = await client.PostAsync(path, list);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Should_Return_OK_When_AccessToken_Is_Valid()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = $"{ROUTE_PATH}/ExternalLoginFacebook";
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accessToken", TestWebUtil.FacebookAccessToken)
            };
            var result = await client.PostAsync(path, list);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }


        [Fact]
        public async Task Should_Return_BadRequest_When_Not_Using_Token()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());            
            var result = await client.GetAsync(ROUTE_PATH);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
