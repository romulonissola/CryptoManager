using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.WebApi.Test.Mocks
{
    public class MockAuthorization
    {
        public async Task<string> GetValidToken()
        {
            HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
            string path = $"/api/Account/ExternalLoginFacebook?accessToken={TestWebUtil.FacebookAccessToken}";
            var result = await client.PostAsync(path);

            return (await result.Content.ReadAsStringAsync()).Replace("\"", "");
        }
    }
}
